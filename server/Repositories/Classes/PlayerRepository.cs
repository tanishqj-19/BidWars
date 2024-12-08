using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class PlayerRepository : IPlayerRepository
    {

        private readonly SportsDbContext context;

        public PlayerRepository(SportsDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            return await context.Players.ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayerByStatus()
        {
            return await context.Players.Where(p => p.Status != "Sold").ToListAsync();
        }
        public async Task AddPlayer(Player newPlayer)
        {
            await context.Players.AddAsync(newPlayer);
            await context.SaveChangesAsync();
        }
        public async Task UpdatePlayer(Player player)
        {
            context.Players.Update(player);


            await context.SaveChangesAsync();

        }

        public async Task DeletePlayer(int playerId)
        {
            var currPlayer = await context.Players.FindAsync(playerId);
            if (currPlayer != null)
            {
                context.Players.Remove(currPlayer);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Player> GetPlayerById(int id)
        {
            var currPlayer = await context.Players.FindAsync(id);

            return currPlayer;
        }


        public async Task<IEnumerable<Player>> GetPlayersBySport(string sport)
        {
            return await context.Players
                .Where(p => p.Sport == sport)
                .ToListAsync();
        }

        public async Task<Player?> GetPlayerBySportAndName(string sport, string name)
        {
            return await context.Players
        .FirstOrDefaultAsync(p => p.Name == name && p.Sport == sport);
        }

        public async Task<IEnumerable<Player>> SearchPlayerByName(string name)
        {
            return await context.Players.Where(x => x.Name.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Player>> SearchPlayerByPosition(string name)
        {
            return await context.Players
                .Where(x => x.Position.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task<Contract> FetchContract(int playerId)
        {
            var playerContract = await context.Contracts.Where(x => x.PlayerId == playerId).ToListAsync();
            
            return playerContract[0];
        }
    }
}
