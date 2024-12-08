using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services.Classes
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IPerformanceRepository performanceRepository;

        public PerformanceService(IPerformanceRepository performanceRepository)
        {
            this.performanceRepository = performanceRepository;
        }

        public async Task<IEnumerable<PerformanceReport>> GetReportsByPlayer(int playerId)
        {
            if (playerId <= 0)
                throw new Exception("Player Id should be positive");

            return await performanceRepository.GetPerformanceReportsByPlayerId(playerId);
        }

        public async Task AddPerformanceReport(PerformanceReport report)
        {
            if (report == null)
                throw new Exception("Report should not be null");

            await performanceRepository.AddPerformanceReport(report);
        }

        public async Task RemovePerformanceReportByPlayer(int playerId)
        {
            if (playerId <= 0)
                throw new Exception("Player Id should be positive");
            await performanceRepository.DeletePerformanceReportByPlayer(playerId);
        }

        public async Task RemoveSinglePerformanceReport(int reportId)
        {
            if (reportId <= 0)
                throw new Exception("Report Id should be positive");
            await performanceRepository.DeleteSingleReport(reportId);
        }
    }
}
