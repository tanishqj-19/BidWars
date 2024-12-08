using server.Models;

namespace server.Services.Interfaces
{
    public interface IBidService
    {
        Task<Bid> GetBidByIdAsync(int bidId);
        Task<IEnumerable<Bid>> GetBidsByAuctionIdAsync(int auctionId);
        //Task AddBidAsync(Bid bid);

        Task<bool> PlaceBid(int auctionId, int playerId, int userId, decimal bidAmount);
        //Task<IEnumerable<AuctionResult>> GenerateAuctionResultsAsync(int auctionId);
        Task<Bid> GetHighestBid(int auctionId, int playerId);
        Task ClosePlayerAuction(Auction auction);
    }
}
