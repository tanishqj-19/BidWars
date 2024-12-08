using server.Dto;
using server.Models;

namespace server.Services.Interfaces
{
    public interface IAuctionService
    {

        Task<IEnumerable<Auction>> GetAllAuctions();
        Task<Auction> GetAuctionById(int auctionId);
        Task<IEnumerable<Auction>> GetAuctionsByStatus(string status);
        Task<Auction> AddAuction(AuctionDto auction);

        Task UpdateAuction(Auction auction);
        Task DeleteAuction(int auctionId);
        Task<bool> PlayerParticipatedInAuctionAsync(int auctionId, int playerId);
        Task<bool> TeamParticipatedInAuctionAsync(int auctionId, int teamId);
        //Task StartAuctionAsync(int auctionId);
        //Task EndAuctionAsync(int auctionId);

    }
}
