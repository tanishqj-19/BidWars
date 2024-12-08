using server.Models;

namespace server.Repositories.Interfaces
{
    public interface ITeamRepository
    {

        Task AddTeam(Team team);
        Task<Team?> GetTeamById(int teamId);
        Task<IEnumerable<Team>> GetAllTeams();

        Task UpdateTeam(Team team);
        Task DeleteTeam(int teamId);
        Task AddPlayerToTeam(int teamId, int playerId);
        Task<IEnumerable<Player>> GetTeamPlayers(int teamId);

        Task<Team> GetTeamByManagerIdAsync(int managerId);  
    }
}
