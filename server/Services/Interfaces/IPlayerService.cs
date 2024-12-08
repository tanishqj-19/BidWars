using server.Models;

namespace server.Services.Interfaces
{
    public interface IPlayerService
    {
        Task AddNewPlayer(Player newPlayer);
        Task<IEnumerable<Player>> SearchPlayer(string name);
        Task UpdatePlayerInformation(Player player);

        Task DeletePlayer(int id);

        Task<IEnumerable<Player>> FilterPlayerBySport(string sport);

        Task<IEnumerable<Player>> GetAllPlayer();

        Task<Player> GetPlayerById(int id);
        Task<Contract> GetPlayerContract(int playerId);

        Task<IEnumerable<Player>> GetPlayerByStatus();
    }
}
