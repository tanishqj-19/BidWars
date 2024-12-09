using Microsoft.AspNetCore.SignalR;
using server.Dto;
using server.Models;
using server.Services.Interfaces;
using System.Text.RegularExpressions;

namespace server.SignalRHub
{

    public class AuctionHub : Hub
    {
        private readonly IBidService _bidService;
        private readonly IAuctionService _auctionService;
        private readonly INotificationService _notificationService;

        public AuctionHub(
            IBidService bidService,
            IAuctionService auctionService,
            INotificationService notificationService)
        {
            _bidService = bidService;
            _auctionService = auctionService;
            _notificationService = notificationService;
        }

        public async Task PlaceBid(int auctionId, int playerId, int userId, decimal bidAmount)
        {
            try
            {
                var bidResult = await _bidService.PlaceBid(auctionId, playerId, userId, bidAmount);
                var message = $"High Bid Alert. A bid of amount {bidAmount} is placed";

                var notification = new Notification
                {
                    UserId = userId,
                    Message = message,
                    Timestamp = DateTime.UtcNow,
                    Type = "Auction",

                };
                await _notificationService.AddNotification(notification);
                // Broadcast bid to all clients in the auction group
                await Clients.Group($"Auction_{auctionId}")
                    .SendAsync("ReceiveBid", new Notification
                    {
                        UserId = userId,
                        Message = message,
                        Timestamp = DateTime.UtcNow,
                        Type = "Auction",

                    });
                
                
            }
            catch (Exception ex)
            {
                // Send error to the specific client
                await Clients.Caller.SendAsync("BidError", ex.Message);
            }
        }

        public async Task CreateAuction(AuctionDto auction)
        {
            try
            {
                var newAuction = await _auctionService.AddAuction(auction);

                // Broadcast new auction to all clients
                await Clients.All.SendAsync("ReceiveNewAuction", new {
                    AuctionId = newAuction.AuctionId,
                    Auctioneer = newAuction.Auctioneer,
                    PlayerId = newAuction.PlayerId,
                    EndTime = newAuction.EndTime,
                    StartTime = newAuction.StartTime,
                    Sport = newAuction.Sport,
                    Date = newAuction.Date,
                    Status = newAuction.Status
                });
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("AuctionCreationError", ex.Message);
            }
        }

        public async Task JoinAuctionRoom(int auctionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Auction_{auctionId}");
        }

        public async Task LeaveAuctionGroup(int auctionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Auction_{auctionId}");
        }
    }
    //public class AuctionHub : Hub
    //{   
    //    private readonly IBidService _bidService;
    //    public AuctionHub(IBidService bidService)
    //    {
    //        _bidService = bidService;
    //    }
    //    public async Task PlaceBid(int auctionId, int playerId, int userId, decimal bidAmount)
    //    {
    //        try
    //        {
    //            await _bidService.PlaceBid(auctionId, playerId, userId, bidAmount);
    //        }
    //        catch (Exception ex)
    //        {

    //            await Clients.Caller.SendAsync("BidError", ex.Message);
    //        }
    //    }

    //    public async Task JoinAuctionRoom(int auctionId)
    //    {
    //        await Groups.AddToGroupAsync(Context.ConnectionId, $"Auction_{auctionId}");
    //    }

    //    // Leave a specific auction group
    //    public async Task LeaveAuctionGroup(int auctionId)
    //    {
    //        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Auction-{auctionId}");
    //    }


    //}
}
