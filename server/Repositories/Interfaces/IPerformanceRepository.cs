using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IPerformanceRepository
    {

        Task<IEnumerable<PerformanceReport>> GetPerformanceReportsByPlayerId(int playerId);
        Task<IEnumerable<PerformanceReport>> GetAllPerformances();
        Task AddPerformanceReport(PerformanceReport newReport);

        Task UpdatePerformanceReport(PerformanceReport newReport);

        Task DeletePerformanceReportByPlayer(int playerId);

        Task DeleteSingleReport(int id);
    }
}
