using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class TeamRepository : ITeamRepository
    {

        private SportsDbContext _context { get; set; }

        public TeamRepository(SportsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _context.Teams
                .Include(t => t.Manager) // Include Manager details
                .Include(t => t.Players) // Include associated players
                .ToListAsync();
        }

        public async Task<Team?> GetTeamById(int teamId)
        {
            return await _context.Teams
                .Include(t => t.Manager)
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.TeamId == teamId);
        }


        public async Task AddTeam(Team newTeam)
        {
            await _context.Teams.AddAsync(newTeam);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeam(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeam(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddPlayerToTeam(int teamId, int playerId)
        {
            var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.TeamId == teamId);
            var player = await _context.Players.FindAsync(playerId);

            if (team == null || player == null)
            {
                throw new InvalidOperationException("Team or player not found.");
            }

            var result = await _context.AuctionResults.FirstOrDefaultAsync(x => x.PlayerId == playerId && x.WinningTeamId == teamId);

            if (result == null)
            {
                throw new InvalidOperationException($"{team.Name} never bought player {player.Name}");
            }
            team.Players.Add(player);
            team.RosterSize++;
            var PlayerPrice = result.FinalPrice;
            team.TotalExpenditure = team.TotalExpenditure + PlayerPrice; // Adjust budget logic as needed

            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Player>> GetTeamPlayers(int teamId)
        {
            var team = await _context.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.TeamId == teamId);

            return team?.Players ?? Enumerable.Empty<Player>();
        }
        public async Task<Team> GetTeamByManagerIdAsync(int managerId)
        {
            return await _context.Teams.FirstOrDefaultAsync(t => t.ManagerId == managerId);   
        }
    }
}
