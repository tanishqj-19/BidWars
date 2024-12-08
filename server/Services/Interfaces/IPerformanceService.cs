using server.Models;

namespace server.Services.Interfaces
{
    public interface IPerformanceService
    {

        Task<IEnumerable<PerformanceReport>> GetReportsByPlayer(int playerId);

        Task AddPerformanceReport(PerformanceReport report);

        Task RemovePerformanceReportByPlayer(int playerId);

        Task RemoveSinglePerformanceReport(int reportId);
    }
}
