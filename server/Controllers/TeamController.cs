using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {

        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Team>>> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        [HttpGet("{teamId}")]
        
        public async Task<ActionResult<Team>> GetTeamById(int teamId)
        {
            try
            {
                var team = await _teamService.GetTeamByIdAsync(teamId);
                return Ok(team);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpPost]
        [Authorize(Roles = "Team Manager")]
        public async Task<ActionResult> CreateTeam(Team team)
        {
            try
            {
                await _teamService.AddTeamAsync(team);
                return CreatedAtAction(nameof(GetTeamById), new { id = team.TeamId }, team);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});  
            }
            
        }
        [HttpPut("{teamId}")]
        [Authorize(Roles = "Team Manager")]

        public async Task<IActionResult> UpdateTeamDetails(int teamId, Team team)
        {

            try
            {
                if (teamId != team.TeamId)
                {
                    throw new Exception("Team invalid");
                }
                await _teamService.UpdateTeamAsync(team);

                return Ok(new { message = "Team updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("players/{teamId}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetTeamPlayers(int teamId)
        {
            var players = await _teamService.GetTeamPlayersAsync(teamId);
            if(players == null)
                return NoContent();
            return Ok(players);
        }
        [HttpPost("{teamId}/players/{playerId}")]
        [Authorize(Roles = "Team Manager")]
        public async Task<ActionResult> AddPlayerToTeam(int teamId, int playerId)
        {
            try
            {
                await _teamService.AddPlayerToTeamAsync(teamId, playerId);
                return Ok(new {message = "Player added to team successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
