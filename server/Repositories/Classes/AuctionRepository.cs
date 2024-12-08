using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly SportsDbContext context;

        public AuctionRepository(SportsDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Auction>> GetAllAuctions()
        {
            return await context.Auctions
                .Include(A => A.Auctioneer)
                .ToListAsync();
        }
        public async Task<Auction> GetAuctionById(int auctionId)
        {
            var currAuction = await context.Auctions.FindAsync(auctionId);

            return currAuction;

        }

        public async Task<IEnumerable<Auction>> GetAuctionsByStatus(string status)
        {
            var auctions = await context.Auctions.Where(a => a.Status == status).ToListAsync();

            return auctions;
        }

        public async Task AddAuction(Auction auction)
        {
            await context.Auctions.AddAsync(auction);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAuction(Auction auction)
        {
            context.Auctions.Update(auction);
            await context.SaveChangesAsync();

        }
        public async Task DeleteAuction(int auctionId)
        {
            var auction = await context.Auctions.FindAsync(auctionId);
            if (auction != null)
            {
                context.Auctions.Remove(auction);
                await context.SaveChangesAsync();
            }
        }
    }
}
