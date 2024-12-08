using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;
using System.Collections.Generic;

namespace server.Services.Classes
{
    public class PlayerService : IPlayerService
    {

        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task AddNewPlayer(Player newPlayer)
        {
            var existingPlayer = await _playerRepository.GetPlayerBySportAndName(newPlayer.Sport, newPlayer.Name);

            if (existingPlayer != null)
            {
                throw new Exception($"{newPlayer.Name} exists with sport ${newPlayer.Sport}");
            }

            await _playerRepository.AddPlayer(newPlayer);


        }

        
        public async Task UpdatePlayerInformation(Player player)
        {
            await _playerRepository.UpdatePlayer(player);

        }

        public async Task<IEnumerable<Player>> FilterPlayerBySport(string sport)
        {
            return await _playerRepository.GetPlayersBySport(sport);
        }

        public async Task<IEnumerable<Player>> GetAllPlayer()
        {
            return await _playerRepository.GetAllPlayers();
        }

        public async Task<Player> GetPlayerById(int id)
        {
            if (id <= 0)
                throw new Exception("Player id should be greater than 0");
            return await _playerRepository.GetPlayerById(id);
        }

        public async Task DeletePlayer(int playerId)
        {
            await _playerRepository.DeletePlayer(playerId);

        }

        public async Task<IEnumerable<Player>> SearchPlayer(string name)
        {
           
            if (string.IsNullOrWhiteSpace(name))
            {
                return Enumerable.Empty<Player>();
            }

            
            var playersByName = await _playerRepository.SearchPlayerByName(name);

            
            if (playersByName == null || !playersByName.Any())
            {
                return await _playerRepository.SearchPlayerByPosition(name);
            }

            return playersByName;
        }

        public async Task<IEnumerable<Player>> GetPlayerByStatus()
        {
            return await _playerRepository.GetPlayerByStatus();
        }

        public async Task<Contract> GetPlayerContract(int playerId)
        {
            if (playerId <= 0)
                throw new Exception("Player Id should be greater than 0");
            
            return await _playerRepository.FetchContract(playerId); 
        }
    }
}
