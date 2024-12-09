using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {

        private readonly IBidService bidService;

        public BidController(IBidService bidService)
        {
            this.bidService = bidService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bid>> GetBidById(int id)
        {
            var lastBid = await bidService.GetBidByIdAsync(id);

            return Ok(new { bid = lastBid });

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bid>>> GetAllBids()
        {
            var bids = await bidService.GetAllBids();

            return Ok(bids);
        }

        [HttpGet("highest/{auctionId}/{playerId}")]
        [Authorize]
        public async Task<IActionResult> GetHighestBid(int auctionId, int playerId)
        {
            var highest = await bidService.GetHighestBid(auctionId, playerId);
            if (highest == null)
                return NoContent();
            return Ok(highest);
        }
        [HttpGet("auction/{auctionId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBidByAuctionId(int auctionId)
        {
            var allBids = await bidService.GetBidsByAuctionIdAsync(auctionId);
            return Ok(allBids);
        }

        //[HttpPost]

        //public async Task<IActionResult> AddBid(Bid newBid)
        //{
        //    if (newBid == null)
        //        return BadRequest("New bid can't be null");

        //    await bidService.AddBid(newBid);

        //    return Ok();
        //}

    }
}
