using server.Models;

namespace server.Services.Interfaces
{
    public interface IFinanceService
    {
        Task<IEnumerable<Finance>> GetTeamFinancesAsync(int teamId);
        Task<Finance> GetFinanceByIdAsync(int financeId);
        Task AddFinanceAsync(Finance finance);
        Task<decimal> GetTeamExpenditureAsync(int teamId);
        Task<decimal> GetRemainingBudgetAsync(int teamId);
        Task LogTransaction(int teamId, string transactionType, decimal amount);
        Task<IEnumerable<Finance>> GetAllFinances();
    }
}
