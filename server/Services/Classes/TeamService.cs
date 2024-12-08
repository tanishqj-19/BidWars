using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services.Classes
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IPlayerService _playerService;

        public TeamService(ITeamRepository teamRepository, IPlayerService playerService)
        {
            _teamRepository = teamRepository;
            _playerService = playerService;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _teamRepository.GetAllTeams();
        }

        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            var currTeam = await _teamRepository.GetTeamById(teamId);

            if (currTeam == null)
            {
                throw new Exception("Team does not exists");
            }
            return currTeam;
        }

        public async Task AddTeamAsync(Team team)
        {

            if (team == null)
            {
                throw new Exception("Team is empty or null");
            }
            await _teamRepository.AddTeam(team);
        }

        public async Task UpdateTeamAsync(Team team)
        {
            if (team == null)
            {
                throw new Exception("Team is empty or null");
            }
            await _teamRepository.UpdateTeam(team);
        }

        public async Task DeleteTeamAsync(int teamId)
        {
            if (teamId <= 0)
                throw new Exception("Team Id must be positive");
            await _teamRepository.DeleteTeam(teamId);
        }

        public async Task AddPlayerToTeamAsync(int teamId, int playerId)
        {

            var player = await _playerService.GetPlayerById(playerId);
            if (player == null || player.Status != "Available")
            {
                throw new InvalidOperationException("Player not available for addition to the team.");
            }


            await _teamRepository.AddPlayerToTeam(teamId, playerId);


            player.Status = "Assigned to Team";

            await _playerService.UpdatePlayerInformation(player);
        }

        public async Task<IEnumerable<Player>> GetTeamPlayersAsync(int teamId)
        {
            var currTeam = await _teamRepository.GetTeamPlayers(teamId);
            if (currTeam == null)
            {
                throw new Exception($"Team with Id {teamId} doesn't exist");
            }
            return currTeam;
        }

        public async Task<Team> GetTeamByManagerId(int managerId)
        {
            return await _teamRepository.GetTeamByManagerIdAsync(managerId);
        }
    }
}
