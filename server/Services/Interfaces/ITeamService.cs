using server.Models;

namespace server.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team> GetTeamByIdAsync(int teamId);
        Task AddTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(int teamId);
        Task AddPlayerToTeamAsync(int teamId, int playerId);
        Task<IEnumerable<Player>> GetTeamPlayersAsync(int teamId);

        Task<Team> GetTeamByManagerId(int managerId);  
    }
}
