using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly IFinanceService _financeService;

        public FinanceController(IFinanceService financeService)
        {
            _financeService = financeService;
        }

        [HttpGet("team/{teamId}")]
        [Authorize(Roles = "Team Manager,Admin")]
        public async Task<ActionResult<IEnumerable<Finance>>> GetTeamFinances(int teamId)
        {
            try
            {
                var finances = await _financeService.GetTeamFinancesAsync(teamId);
                return Ok(finances);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        //[HttpGet]
        //public async Task<IActionResult<>>
        //[HttpPost]
        //public async Task<ActionResult> AddFinance(Finance finance)
        //{
        //    try
        //    {
        //        await _financeService.AddFinanceAsync(finance);
        //        return Ok("Transaction added successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpGet("team/{teamId}/remaining-budget")]
        //public async Task<ActionResult<decimal>> GetRemainingBudget(int teamId)
        //{
        //    var remainingBudget = await _financeService.GetRemainingBudgetAsync(teamId);
        //    return Ok(new {budget = remainingBudget });
        //}
    }
}
