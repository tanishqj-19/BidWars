using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IAuctionRepository
    {
        Task<IEnumerable<Auction>> GetAllAuctions();
        Task<Auction> GetAuctionById(int auctionId);

        Task<IEnumerable<Auction>> GetAuctionsByStatus(string status);

        Task AddAuction(Auction auction);
        Task UpdateAuction(Auction auction);
        Task DeleteAuction(int auctionId);
    }
}
