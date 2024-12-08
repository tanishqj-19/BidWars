using Microsoft.AspNetCore.SignalR;
using server.SignalRHub;
using System.Timers;
using System.Threading;
using server.Services.Interfaces;

namespace server.Services.Classes
{
    public class LiveAuctionUpdation : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private System.Threading.Timer _timer;
        private readonly IHubContext<AuctionHub> _hubContext;

        public LiveAuctionUpdation(IServiceScopeFactory scopeFactory, IHubContext<AuctionHub> hubContext)
        {
            _hubContext = hubContext;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new System.Threading.Timer(CheckAndCloseExpiredAuctions, null,TimeSpan.Zero,
            TimeSpan.FromMinutes(1)); 
            return Task.CompletedTask;

        }
        
        private async void CheckAndCloseExpiredAuctions(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {   
                var auctionService = scope.ServiceProvider.GetRequiredService<IAuctionService>();
                var bidService = scope.ServiceProvider.GetRequiredService<IBidService>();
                var playerService = scope.ServiceProvider.GetRequiredService<IPlayerService>();

                var auctions = await auctionService.GetAllAuctions();

                var currentTime = DateTime.Now;
                foreach (var auction in auctions)
                {
                    if(auction.Status == "Scheduled" && auction.StartTime <= currentTime)
                    {
                        auction.Status = "Ongoing";
                        await auctionService.UpdateAuction(auction);
                    }else if(auction.Status == "Ongoing" && auction.EndTime <= currentTime)
                    {
                        auction.Status = "Completed";
                        await auctionService.UpdateAuction(auction);
                        await bidService.ClosePlayerAuction(auction);

                        await _hubContext.Clients.All.SendAsync("AuctionClosed : ", auction.AuctionId);
                    }
                    

                }

            }
        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
