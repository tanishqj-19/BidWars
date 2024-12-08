using server.Models;

namespace server.Services.Interfaces
{
    public interface IAuctionResultService
    {
        Task<IEnumerable<AuctionResult>> GetAllAuctionResultsAsync();
        Task<AuctionResult> GetAuctionResultByIdAsync(int resultId);
        Task<IEnumerable<AuctionResult>> GetAuctionResultsByAuctionIdAsync(int auctionId);
        Task AddAuctionResultAsync(AuctionResult auctionResult);
        Task UpdateAuctionResultAsync(AuctionResult auctionResult);
        Task DeleteAuctionResultAsync(int resultId);

        Task<IEnumerable<AuctionResult>> GetAuctionResultByStatus(string status);
    }
}
