using AutoMapper;
using Azure;
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
    public class PerformanceReportController : ControllerBase
    {
        private readonly IPerformanceService performanceService;
        //private readonly IMapper _mapper;

        public PerformanceReportController(IPerformanceService performanceService)
        {
            this.performanceService = performanceService;
            //_mapper = mapper;
        }

        [HttpGet("{playerId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PerformanceReport>>> GetPlayerReports(int playerId)
        {
            try
            {
                var reports = await performanceService.GetReportsByPlayer(playerId);
                return Ok(new { reports = reports });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]

        public async Task<IActionResult> AddNewPerformance([FromBody] ReportDto newReport)
        {
            if (newReport == null)
            {   
                
                return BadRequest(new { message = "Report is null" });
            }
            try
            {
                PerformanceReport report = new PerformanceReport();

                report.Sport = newReport.Sport;
                report.Tournament = newReport.Tournament;
                report.PlayerId = newReport.PlayerId;
                report.MatchDate = newReport.MatchDate;
                report.MatchType = newReport.MatchType;
                report.Opponent = newReport.Opponent;
                report.Rating = newReport.Rating;
                report.AnalystId = newReport.AnalystId;
                report.Stats1 = newReport.Stats1;
                report.Stats2 = newReport.Stats2;
                report.Stats3 = newReport.Stats3;
                report.Stats4 = newReport.Stats4;
                //var report = _mapper.Map<PerformanceReport>(newReport);
                await performanceService.AddPerformanceReport(report);
                return Content("Added Successfully");

            }catch(Exception e)
            {
                return BadRequest(new {message = e.Message});
            }
        }
    }
}
