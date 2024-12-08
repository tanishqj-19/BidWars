using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IBidRepository
    {

        Task<Bid?> GetBidById(int bidId);
        Task<IEnumerable<Bid>> GetBidsByAuctionId(int auctionId);
        Task<IEnumerable<Bid>> GetBidsByPlayerId(int playerId);
        Task AddBid(Bid bid);
        Task UpdateBid(Bid bid);
        Task DeleteBid(int bidId);
        Task<Bid?> GetHighestBidForPlayerAsync(int auctionId, int playerId);
        Task<IEnumerable<Bid>> GetBidsForTeamAsync(int teamId);
        Task GenerateContract(Contract newContract);
    }
}
