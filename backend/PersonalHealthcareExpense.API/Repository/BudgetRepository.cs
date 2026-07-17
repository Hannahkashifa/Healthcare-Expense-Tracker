using Microsoft.EntityFrameworkCore;
using PersonalHealthcareExpense.API.Data;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Repository
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly ApplicationDbContext _context;

        public BudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Budget>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Budgets.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<Budget?> GetByIdAsync(int id)
        {
            return await _context.Budgets.FindAsync(id);
        }

        public async Task<Budget?> GetByCategoryMonthYearAsync(int userId, string category, int month, int year)
        {
            return await _context.Budgets
                .FirstOrDefaultAsync(b => b.UserId == userId
                    && b.Category == category
                    && b.Month == month
                    && b.Year == year);
        }

        public async Task AddAsync(Budget budget)
        {
            await _context.Budgets.AddAsync(budget);
        }

        public async Task UpdateAsync(Budget budget)
        {
            _context.Budgets.Update(budget);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);

            if (budget != null)
            {
                _context.Budgets.Remove(budget);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
