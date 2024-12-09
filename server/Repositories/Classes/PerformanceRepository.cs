using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class PerformanceRepository : IPerformanceRepository
    {
        private readonly SportsDbContext _context;

        public PerformanceRepository(SportsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PerformanceReport>> GetPerformanceReportsByPlayerId(int playerId)
        {
            var reports = await _context.PerformanceReports.Where(pr => pr.PlayerId == playerId).ToListAsync();

            return reports;
        }

        public async Task AddPerformanceReport(PerformanceReport newReport)
        {
            await _context.PerformanceReports.AddAsync(newReport);

            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<PerformanceReport>> GetAllPerformances()
        {
            return await _context.PerformanceReports.ToListAsync();
        }



        public async Task UpdatePerformanceReport(PerformanceReport newReport)
        {
            _context.PerformanceReports.Update(newReport);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePerformanceReportByPlayer(int playerId)
        {
            var reports = await GetPerformanceReportsByPlayerId(playerId);
            foreach (var report in reports)
            {
                _context.PerformanceReports.Remove(report);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSingleReport(int id)
        {
            var report = await _context.PerformanceReports.FindAsync(id);

            if (report != null)
            {
                _context.PerformanceReports.Remove(report);

            }

            await _context.SaveChangesAsync();
        }
    }
}
