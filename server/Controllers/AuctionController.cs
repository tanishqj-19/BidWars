using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;
        private readonly IBidService _bidService;
        //private readonly IMapper _mapper;

        public AuctionController(IAuctionService auctionService, IBidService bidService)
        {
            _auctionService = auctionService;
            _bidService = bidService;
            //_mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAllAuctions()
        {
            var auctions = await _auctionService.GetAllAuctions();
            return Ok(auctions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Auction>> GetAuctionById(int id)
        {
            var auction = await _auctionService.GetAuctionById(id);
            if (auction == null)
                return NotFound();

            return Ok(auction);
        }
        
        [HttpGet("ongoing")]
        [Authorize(Policy="All")]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuctionByOngoing()
        {
            var auctions = await _auctionService.GetAuctionsByStatus("Ongoing");
            return Ok(auctions);
        }
        
        [HttpGet("scheduled")]
        [Authorize(Roles = "Analyst,Player Agent,Team Manager,Admin,Auctioneer")]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuctionByScheduled()
        {
            var auctions = await _auctionService.GetAuctionsByStatus("Scheduled");
            return Ok(auctions);
        }


        
        [HttpGet("completed")]
        [Authorize(Roles = "Admin,Auctioneer")]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuctionByCompleted()
        {
            var auctions = await _auctionService.GetAuctionsByStatus("Completed");
            return Ok(auctions);
        }


        // /api/auctin/create
        //[HttpPost("create")]
        //public async Task<ActionResult> CreateAuction( AuctionDto newAuctionDto)
        //{   
        //    if(newAuctionDto == null || !ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        var auction = await _auctionService.AddAuction(newAuctionDto);
        //        //await _auctionService.StartAuctionAsync(auction.AuctionId);
        //        return CreatedAtAction(nameof(GetAuctionById), new { id = auction.AuctionId }, auction);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }


        //}

    }
}
