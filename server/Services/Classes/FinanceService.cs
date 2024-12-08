using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services.Classes
{
    public class FinanceService : IFinanceService
    {
        private readonly IFinanceRepository _financeRepository;
        private readonly ITeamRepository _teamRepository;

        public FinanceService(IFinanceRepository financeRepository, ITeamRepository teamRepository)
        {
            _financeRepository = financeRepository;
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<Finance>> GetTeamFinancesAsync(int teamId)
        {
            if (teamId <= 0)
                throw new Exception("Team Id should be positive");
            return await _financeRepository.GetFinancesByTeamId(teamId);
        }

        public async Task<Finance> GetFinanceByIdAsync(int financeId)
        {
            return await _financeRepository.GetFinanceById(financeId);
        }

        public async Task AddFinanceAsync(Finance finance)
        {
            
            var team = await _teamRepository.GetTeamById(finance.TeamId);
            if (team == null)
            {
                throw new KeyNotFoundException("Team not found.");
            }

            
            if (finance.TransactionType == "Purchase")
            {
                var remainingBudget = await GetRemainingBudgetAsync(finance.TeamId);
                if (remainingBudget < finance.Amount)
                {
                    throw new InvalidOperationException("Insufficient budget for this purchase.");
                }
            }

            await _financeRepository.AddFinance(finance);
        }

        public async Task<decimal> GetTeamExpenditureAsync(int teamId)
        {
            return await _financeRepository.GetTotalExpenditure(teamId);
        }

        public async Task<decimal> GetRemainingBudgetAsync(int teamId)
        {
            return await _financeRepository.GetRemainingBudget(teamId);
        }

        public async Task LogTransaction(int teamId, string transactionType, decimal amount)
        {
            var financelog = new Finance
            {
                TeamId = teamId,
                TransactionType = transactionType,
                Amount = amount,
                Date = DateTime.UtcNow,
                Details = $"{transactionType} of {amount:C}"
            };

            await AddFinanceAsync(financelog);
        }
    }
}
