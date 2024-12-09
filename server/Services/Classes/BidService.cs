using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using server.Models;
using server.Repositories.Classes;
using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.SignalRHub;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace server.Services.Classes
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        private readonly IPlayerService _playerService;
        private readonly IHubContext<AuctionHub> _hubContext;
        private readonly IFinanceService _financeService;
        private readonly ITeamService _teamService;
        private readonly INotificationService _notificationService;
        private readonly IAuctionService _auctionService;
        private readonly IAuctionResultService _auctionResultService;
        private static readonly decimal BonusPercentage = 0.2m;
        private static readonly decimal auctionFee = 0.025m;
        private readonly IUserService _userService;

        public BidService(IBidRepository bidRepository, IFinanceService financeService,
            IHubContext<AuctionHub> hubContext, IPlayerService playerService,
            ITeamService teamService, IAuctionService auctionService,
            IAuctionResultService auctionResultService, INotificationService notificationService, IUserService userService)
        {
            _bidRepository = bidRepository;
            _playerService = playerService;
            _hubContext = hubContext;
            _financeService = financeService;
            _teamService = teamService;
            _auctionService = auctionService;
            _auctionResultService = auctionResultService;
            _notificationService =  notificationService;
            _userService   = userService;
        }

        public async Task<Bid> GetBidByIdAsync(int bidId)
        {
            if (bidId <= 0)
                throw new Exception("Bid Id must be positive");
            var currBid = await _bidRepository.GetBidById(bidId);

            if (currBid == null)
            {
                throw new Exception("Bid with id doesn't exists");
            }
            return currBid;
        }

        public async Task<IEnumerable<Bid>> GetBidsByAuctionIdAsync(int auctionId)
        {
            if (auctionId <= 0)
                throw new Exception("Auction Id must be positive");
            var specificBids = await _bidRepository.GetBidsByAuctionId(auctionId);

            if (specificBids == null)
            {
                throw new Exception("Bids for auction Id doesn't exists");
            }
            return specificBids;
        }

        //public async Task AddBidAsync(Bid bid)
        //{
        //    if (bid == null)
        //        throw new Exception("Bid can't be empty");
        //    await _bidRepository.AddBid(bid);
        //}

        public async Task<bool> PlaceBid(int auctionId, int playerId, int userId, decimal bidAmount)
        {

            var auction = await _auctionService.GetAuctionById(auctionId);

            if (auction == null)
                throw new Exception("Auction can't be null");
            if (auction.Status != "Ongoing")
            {
                throw new InvalidOperationException("Auction is not currently active");
            }
            // Validate current highest bid
            var currentHighestBid = await _bidRepository.GetHighestBidForPlayerAsync(auctionId, playerId);

            if (currentHighestBid != null && bidAmount <= currentHighestBid.BidAmount)
            {
                throw new InvalidOperationException("Bid amount must be higher than the current highest bid.");
            }

            var team = await _teamService.GetTeamByManagerId(userId);

            if (bidAmount > team.Budget)
            {
                throw new Exception("Insufficient team budget for this bid");

            }
            // Create the bid
            var newBid = new Bid
            {
                AuctionId = auctionId,
                PlayerId = playerId,
                TeamId = team.TeamId,
                BidAmount = bidAmount,
                BidTime = DateTime.UtcNow,
                Status = "Highest"
            };

            // Save the bid
            try
            {
                await _bidRepository.AddBid(newBid);
                await _financeService.LogTransaction(team.TeamId, "Bid", bidAmount);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }


            var users = await _userService.GetAllUsers();
            users = users.Where(x=> x.Role == "Team Manager" || x.UserId == userId).ToList();

            foreach(var u in users)
            {
                var message = $"High Bid Alert. A bid of amount {bidAmount} is placed by {team.Name}";

                var notification = new Notification
                {
                    UserId = u.UserId,
                    Message = message,
                    Timestamp = DateTime.UtcNow,
                    Type = "Auction",

                };
                await _notificationService.AddNotification(notification);
            }

            return true;
        }

        public async Task<Bid> GetHighestBid(int auctionId, int playerId)
        {
            return await _bidRepository.GetHighestBidForPlayerAsync(auctionId, playerId);
        }
        public async Task ClosePlayerAuction(Auction auction)
        {
            var auctionId = auction.AuctionId;
            var playerId = auction.PlayerId; 
            var highestBid = await _bidRepository.GetHighestBidForPlayerAsync(auctionId, playerId);

            if (highestBid != null)
            {
                var player = await _playerService.GetPlayerById(playerId);
                var team = await _teamService.GetTeamByIdAsync(highestBid.TeamId);

                if (team.Budget < highestBid.BidAmount)
                {
                    await _bidRepository.DeleteBid(highestBid.BidId);
                    throw new Exception("Team Budget is lesser than bidAmount. Bill Invalid");
                }

                User manager = team.Manager;
                    
                
                // Create auction result
                var result = new AuctionResult
                {
                    AuctionId = auctionId,
                    PlayerId = playerId,
                    WinningTeamId = highestBid.TeamId,
                    BasePrice = player.BasePrice,
                    FinalPrice = highestBid.BidAmount,
                    Status = "Completed"
                };
                player.Status = "Sold";
                player.TeamId = highestBid.TeamId;
                team.RosterSize += 1;
                team.Budget -= highestBid.BidAmount;
                team.Budget -= auctionFee * highestBid.BidAmount;
                team.TotalExpenditure += highestBid.BidAmount;

                var playerContract = new server.Models.Contract
                {
                    PlayerId = player.PlayerId,
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(365)),
                    Salary = highestBid.BidAmount,
                    Bonuses = highestBid.BidAmount * BonusPercentage,
                    Details = $"{player.Name} is sold to the team {team.Name}"
                };
                var notification = new Notification
                {
                    Message = $"Congratulations {player.Name} of {player.Country} has been added to your team.",
                    Type = "Auction Closed",
                    Timestamp = DateTime.Now,
                    UserId = team.ManagerId,

                };

                await _auctionResultService.AddAuctionResultAsync(result);
                await _teamService.UpdateTeamAsync(team);
                await _bidRepository.GenerateContract(playerContract);
                await _financeService.LogTransaction(highestBid.TeamId, "Player Purchase", highestBid.BidAmount);
                await _financeService.LogTransaction(highestBid.TeamId, "Auction Fee", auctionFee * highestBid.BidAmount);
                await _playerService.UpdatePlayerInformation(player);

                await _notificationService.SendBidWinningConfirmation(manager, player, auction);
                await _notificationService.AddNotification(notification);

                var users = await _userService.GetAllUsers();

                foreach(var u in users)
                {
                    var noti = new Notification
                    {
                        UserId = u.UserId,
                        Message = $"Hi {u.Username}, the auction for {player.Name} is over! {team.Name} secured the winning bid. ",
                        Type = "Auction Closed",
                        Timestamp = DateTime.Now,
                    };
                    await _notificationService.AddNotification(noti);
                }
                

                // Notify stakeholders
                //await _notificationService.NotifyAuctionClosed(result);
                

            }



        }

        public async Task<IEnumerable<Bid>> GetAllBids()
        {
            return await _bidRepository.GetAllBids();
        }


    }
}
