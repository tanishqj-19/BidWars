using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IAuctionResultRepository
    {
        Task<IEnumerable<AuctionResult>> GetAllAuctionResults();
        Task<AuctionResult?> GetAuctionResultById(int resultId);
        Task<IEnumerable<AuctionResult>> GetAuctionResultsByAuctionId(int auctionId);
        Task AddAuctionResult(AuctionResult auctionResult);
        Task UpdateAuctionResult(AuctionResult auctionResult);
        Task DeleteAuctionResult(int resultId);

        Task<IEnumerable<AuctionResult>> AuctionResultByStatus(string status);
    }
}
