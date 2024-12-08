using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Task AddPlayer(Player newPlayer);
        Task UpdatePlayer(Player player);

        Task DeletePlayer(int id);
        Task<IEnumerable<Player>> GetAllPlayers();


        Task<IEnumerable<Player>> GetPlayersBySport(string sport);
        Task<Player> GetPlayerById(int id);

        Task<Player?> GetPlayerBySportAndName(string sport, string name);

        Task<IEnumerable<Player>> SearchPlayerByPosition(string name);

        Task<IEnumerable<Player>> SearchPlayerByName(string name);

        Task<Contract> FetchContract(int playerId);

        Task<IEnumerable<Player>> GetPlayerByStatus();


    }
}
