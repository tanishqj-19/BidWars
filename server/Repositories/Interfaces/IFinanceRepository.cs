using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IFinanceRepository
    {
        Task<IEnumerable<Finance>> GetAllFinances();
        Task<IEnumerable<Finance>> GetFinancesByTeamId(int teamId);
        Task<Finance> GetFinanceById(int financeId);
        Task AddFinance(Finance finance);
        Task<decimal> GetTotalExpenditure(int teamId);
        Task<decimal> GetRemainingBudget(int teamId);

       

        
    }
}
