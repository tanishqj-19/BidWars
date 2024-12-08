using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using server.Dto;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.SignalRHub;

namespace server.Services.Classes
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository auctionRepository;
        private readonly IAuctionResultService auctionResultService;
        private readonly INotificationService notificationService;
        private readonly IHubContext<AuctionHub> _hubContext;
        

        public AuctionService(IAuctionRepository auctionRepository,
             IAuctionResultService auctionResultService,
            INotificationService notificationService,
            IHubContext<AuctionHub> hubContext)
        {
            this.auctionRepository = auctionRepository;
            this.auctionResultService = auctionResultService;
            this.notificationService = notificationService;
            _hubContext = hubContext;
        }



        public async Task<IEnumerable<Auction>> GetAllAuctions()
        {
            return await auctionRepository.GetAllAuctions();
        }

        public async Task<Auction> GetAuctionById(int auctionId)
        {
            var currAuction = await auctionRepository.GetAuctionById(auctionId);
            if (currAuction == null)
            {
                throw new Exception("No Auction Exists with this id");
            }
            return currAuction;
        }

        public async Task<IEnumerable<Auction>> GetAuctionsByStatus(string status)
        {


            var currAuctions = await auctionRepository.GetAuctionsByStatus(status);

            return currAuctions;
        }
        public async Task<Auction> AddAuction(AuctionDto auction)
        {
            if (auction.EndTime <= auction.StartTime)
            {
                throw new Exception("Cannot create an auction with start time is greater than end time");
            }

            if (auction.Status != "Ongoing" && auction.Status != "Scheduled" && auction.Status != "Completed")
            {
                throw new Exception("Auction Status is invalid");
            }
            if (auction.EndTime <= DateTime.Now)
                throw new Exception("Please enter valid end time for auction to happen");
            var newAuction = new Auction
            {
                Date = auction.Date,
                Sport = auction.Sport,
                AuctioneerId = auction.AuctioneerId,
                PlayerId = auction.PlayerId,
                StartTime = auction.StartTime,
                EndTime = auction.EndTime,
                Status = auction.Status,
            };
            await auctionRepository.AddAuction(newAuction);

            

            
            await _hubContext.Clients.All.SendAsync("NewAuctionCreated", auction);
            return newAuction;
        }

        public async Task UpdateAuction(Auction auction)
        {
            if (auction.EndTime <= auction.StartTime)
            {
                throw new Exception("Cannot create an auction with start time is greater than end time");
            }
            
            await auctionRepository.UpdateAuction(auction);
        }
        public async Task DeleteAuction(int auctionId)
        {
            var currAuction = await auctionRepository.GetAuctionById(auctionId);

            if (currAuction == null)
                throw new Exception("No auction exists with the given id");
            if (currAuction.EndTime > DateTime.Now)
                throw new Exception("Deletion of onGoing auction is not allowed. Please wait for the auction to complete");

            await auctionRepository.DeleteAuction(auctionId);
        }

        public async Task<bool> PlayerParticipatedInAuctionAsync(int auctionId, int playerId)
        {
            var auction = await auctionRepository.GetAuctionById(auctionId);
            return auction?.Bids.Any(b => b.PlayerId == playerId) ?? false;
        }

        public async Task<bool> TeamParticipatedInAuctionAsync(int auctionId, int teamId)
        {
            var auction = await auctionRepository.GetAuctionById(auctionId);
            return auction?.Bids.Any(b => b.TeamId == teamId) ?? false;
        }

        //public async Task StartAuctionAsync(int auctionId)
        //{
        //    var auction = await auctionRepository.GetAuctionById(auctionId);
        //    if (auction == null)
        //        throw new InvalidOperationException("Auction not found.");

        //    if (auction.Status != "Scheduled")
        //        throw new InvalidOperationException("Auction cannot be started.");

        //    auction.Status = "Ongoing";
        //    auction.StartTime = DateTime.Now;
        //    await auctionRepository.UpdateAuction(auction);

        //    // Notify clients
        //    await _hubContext.Clients.Group($"Auction-{auctionId}").SendAsync("AuctionStarted", new
        //    {
        //        AuctionId = auctionId,
        //        StartTime = DateTime.UtcNow
        //    });

        //    string message = $"Auction {auctionId} has started!";
        //    await notificationService.AddAndBroadcastNotification(
        //        message: message,
        //        group: auctionId.ToString()
        //    );
        //}

        //public async Task EndAuctionAsync(int auctionId)
        //{
        //    var auction = await auctionRepository.GetAuctionById(auctionId);
        //    if (auction == null)
        //        throw new InvalidOperationException("Auction not found.");

        //    if (auction.Status != "Ongoing")
        //        throw new InvalidOperationException("Auction is not currently active.");

        //    auction.Status = "Completed";
        //    auction.EndTime = DateTime.Now;
        //    await auctionRepository.UpdateAuction(auction);

        //    var results = await bidService.GenerateAuctionResultsAsync(auctionId);
        //    foreach (var result in results)
        //    {
        //        await auctionResultService.AddAuctionResultAsync(result);
        //        if(result.Status == "Sold")
        //        {
        //            int winTeamId = result.WinningTeamId.Value;

        //            await financeService.LogTransaction(winTeamId, "Player Purchase", result.FinalPrice);
        //            var teamManagerId = (await teamService.GetTeamByIdAsync(winTeamId)).ManagerId;
        //            var playerPurchaseMessage = $"Congratulations! You successfully purchased Player ID: {result.PlayerId} for {result.FinalPrice:C}.";


        //            await notificationService.AddAndBroadcastNotification(
        //                    message: playerPurchaseMessage,
        //                    userId: teamManagerId
        //                );
        //        }
        //    }


        //    // Notify clients
    //    await _hubContext.Clients.Group($"Auction-{auctionId}").SendAsync("AuctionEnded", new
    //        {
    //            AuctionId = auctionId,
    //            EndTime = DateTime.UtcNow
    //});

        //    string endMessage = $"Auction {auctionId} has ended.";
        //    await notificationService.AddAndBroadcastNotification(
        //        message: endMessage,
        //        group: auctionId.ToString()
        //    );

        //    //foreach (var connection in _hubContext.Clients)
        //    //{
        //    //    await _hubContext.Groups.RemoveFromGroupAsync(connection.ToString(), $"Auction-{auctionId}");
        //    //}
        //}

    }
}
