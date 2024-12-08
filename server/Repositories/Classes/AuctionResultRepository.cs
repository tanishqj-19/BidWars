using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class AuctionResultRepository : IAuctionResultRepository
    {
        private readonly SportsDbContext _context;

        public AuctionResultRepository(SportsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuctionResult>> GetAllAuctionResults()
        {
            return await _context.AuctionResults
                .Include(ar => ar.Auction) // Include Auction details
                .Include(ar => ar.Player) // Include Player details
                .Include(ar => ar.WinningTeam) // Include Winning Team details
                .ToListAsync();
        }

        public async Task<AuctionResult?> GetAuctionResultById(int resultId)
        {
            return await _context.AuctionResults
                .Include(ar => ar.Auction)
                .Include(ar => ar.Player)
                .Include(ar => ar.WinningTeam)
                .FirstOrDefaultAsync(ar => ar.ResultId == resultId);
        }

        public async Task<IEnumerable<AuctionResult>> GetAuctionResultsByAuctionId(int auctionId)
        {
            return await _context.AuctionResults
                .Include(ar => ar.Player)
                .Include(ar => ar.WinningTeam)
                .Where(ar => ar.AuctionId == auctionId)
                .ToListAsync();
        }

        public async Task AddAuctionResult(AuctionResult auctionResult)
        {
            await _context.AuctionResults.AddAsync(auctionResult);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuctionResult(AuctionResult auctionResult)
        {
            _context.AuctionResults.Update(auctionResult);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuctionResult(int resultId)
        {
            var auctionResult = await _context.AuctionResults.FindAsync(resultId);
            if (auctionResult != null)
            {
                _context.AuctionResults.Remove(auctionResult);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AuctionResult>> AuctionResultByStatus(string status)
        {
            var auctionResults = await _context.AuctionResults.Where(ar => ar.Status == status)
                                   .ToListAsync();

            return auctionResults;
        }


    }
}
