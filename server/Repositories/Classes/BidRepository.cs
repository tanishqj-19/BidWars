using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class BidRepository : IBidRepository
    {
        private readonly SportsDbContext _context;

        public BidRepository(SportsDbContext context)
        {
            _context = context;
        }

        public async Task<Bid?> GetBidById(int bidId)
        {
            return await _context.Bids
                .Include(b => b.Auction)
                .Include(b => b.Player)
                .Include(b => b.Team)
                .FirstOrDefaultAsync(b => b.BidId == bidId);
        }

        public async Task<IEnumerable<Bid>> GetBidsByAuctionId(int auctionId)
        {
            return await _context.Bids
            .Where(b => b.AuctionId == auctionId)
            .OrderByDescending(b => b.BidAmount)
            .ToListAsync();
        }

        public async Task<IEnumerable<Bid>> GetBidsForTeamAsync(int teamId)
        {
            return await _context.Bids
                .Where(b => b.TeamId == teamId)
                .OrderByDescending(b => b.BidTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bid>> GetBidsByPlayerId(int playerId)
        {
            return await _context.Bids
                .Where(b => b.PlayerId == playerId)
                .ToListAsync();
        }

        public async Task AddBid(Bid bid)
        {
           
            await _context.Bids.AddAsync(bid);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBid(Bid bid)
        {
            _context.Bids.Update(bid);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBid(int bidId)
        {
            var bid = await GetBidById(bidId);
            if (bid != null)
            {
                _context.Bids.Remove(bid);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Bid?> GetHighestBidForPlayerAsync(int auctionId, int playerId)
        {
            return await _context.Bids
                .Where(b => b.AuctionId == auctionId && b.PlayerId == playerId)
                .OrderByDescending(b => b.BidAmount)
                .FirstOrDefaultAsync();
        }

        public async Task GenerateContract(Contract newContract)
        {
            await _context.Contracts.AddAsync(newContract);
            await _context.SaveChangesAsync();
        }

    }
}
