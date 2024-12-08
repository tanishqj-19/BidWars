using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService playerService;
        private readonly IPerformanceRepository performanceRepository;
        public PlayerController(IPlayerService playerService, IPerformanceRepository performanceRepository) {
            this.playerService = playerService;
            this.performanceRepository = performanceRepository; 
        }


        [HttpGet]

        public async Task<IActionResult> GetAllPlayers()
        {
            var allPlayer = await playerService.GetAllPlayer();
            return Ok(allPlayer);
        }

        [HttpGet("sport")]
        [Authorize]
        public async Task<IActionResult> FilterPlayerBySport(string sport)
        {
            if (string.IsNullOrEmpty(sport))
            {
                return BadRequest("Sport is empty or null");
            }
            var allPlayer = await playerService.FilterPlayerBySport(sport);

            return Ok(allPlayer);
        }
        [HttpGet("search/{name}")]
        //[Authorize]
        public async Task<IActionResult> SearchPlayer(string name)
        {
            var players = await playerService.SearchPlayer(name);

            return Ok(players);
        }

        [HttpGet("contract/{playerId}")]
        [Authorize(Roles = "Admin, Player Agent,Team Manager")]

        public async Task<IActionResult> GetPlayerContract(int playerId)
        {
            var contract = await playerService.GetPlayerContract(playerId);

            if(contract == null)
                return NoContent();
            return Ok(contract);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPlayerById(int Id)
        {
            try
            {
                var currPlayer = await playerService.GetPlayerById(Id);
                return Ok(currPlayer);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Player Agent")]
        public async Task<IActionResult> AddPlayer(Player newPlayer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await playerService.AddNewPlayer(newPlayer);
            return CreatedAtAction(nameof(GetPlayerById), new { Id = newPlayer.PlayerId }, newPlayer);

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlayer(Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await playerService.UpdatePlayerInformation(player);

            return NoContent(); 
        }

        [HttpDelete("{Id}")]
        
        public async Task<IActionResult> DeletePlayer(int Id)
        {
            await playerService.DeletePlayer(Id);
            return Ok("Player successfully deleted");
        }

        [HttpGet("reports/{playerId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PerformanceReport>>> GetPlayerReports(int playerId)
        {
            if (playerId <= 0)
                return BadRequest("Player id should be positive");
            
            var reports = await performanceRepository.GetPerformanceReportsByPlayerId(playerId);

            if (reports == null)
                return NoContent();
            return Ok(reports);
        }

        [HttpGet("available")]

        public async Task<ActionResult<IEnumerable<Player>>> GetAvailablePlayers()
        {
            var players = await playerService.GetPlayerByStatus();

            return Ok(players);
        }

    }
}
