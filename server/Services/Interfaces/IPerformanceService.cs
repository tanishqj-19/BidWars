using server.Models;

namespace server.Services.Interfaces
{
    public interface IPerformanceService
    {

        Task<IEnumerable<PerformanceReport>> GetReportsByPlayer(int playerId);

        Task AddPerformanceReport(PerformanceReport report);

        Task RemovePerformanceReportByPlayer(int playerId);

        Task<IEnumerable<PerformanceReport>> GetAllPerformances();
        Task RemoveSinglePerformanceReport(int reportId);
    }
}
