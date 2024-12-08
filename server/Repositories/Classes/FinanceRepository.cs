using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class FinanceRepository : IFinanceRepository
    {
        private readonly SportsDbContext _context;

        public FinanceRepository(SportsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Finance>> GetFinancesByTeamId(int teamId)
        {
            return await _context.Finances
                .Where(f => f.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<Finance> GetFinanceById(int financeId)
        {
            return await _context.Finances.FindAsync(financeId);
        }

        public async Task AddFinance(Finance finance)
        {
            await _context.Finances.AddAsync(finance);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalExpenditure(int teamId)
        {
            return await _context.Finances
                .Where(f => f.TeamId == teamId && f.TransactionType == "Purchase")
                .SumAsync(f => f.Amount);
        }

        public async Task<decimal> GetRemainingBudget(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            var totalExpenditure = await GetTotalExpenditure(teamId);

            return team.Budget - totalExpenditure;
        }

        
    }
}
