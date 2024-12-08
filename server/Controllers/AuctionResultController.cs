using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionResultController : ControllerBase
    {
        private readonly IAuctionResultService _auctionResultService;

        public AuctionResultController(IAuctionResultService auctionResultService)
        {
            _auctionResultService = auctionResultService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionResult>>> GetAllAuctionResults()
        {
            var results = await _auctionResultService.GetAllAuctionResultsAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionResult>> GeTAuctionResultById(int id)
        {
            try
            {
                var results = await _auctionResultService.GetAuctionResultByIdAsync(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddAuctionResult(AuctionResult auctionResult)
        {
            try
            {
                await _auctionResultService.AddAuctionResultAsync(auctionResult);
                return CreatedAtAction(nameof(GeTAuctionResultById), new { id = auctionResult.ResultId }, auctionResult);
            }catch (Exception ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResult(int id, AuctionResult auctionResult)
        {
            if (id != auctionResult.ResultId)
                return BadRequest("Invalid auction result");
            try
            {
                await _auctionResultService.UpdateAuctionResultAsync(auctionResult);
                return Ok();
            }catch (Exception ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            
            try
            {
                await _auctionResultService.DeleteAuctionResultAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
