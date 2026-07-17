using Microsoft.EntityFrameworkCore;
using PersonalHealthcareExpense.API.Data;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Repository
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly ApplicationDbContext _context;

        public IncomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Income>> GetAllAsync()
        {
            return await _context.Incomes.ToListAsync();
        }

        public async Task<IEnumerable<Income>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Incomes.Where(i => i.UserId == userId).ToListAsync();
        }

        public async Task<Income?> GetByIdAsync(int id)
        {
            return await _context.Incomes.FindAsync(id);
        }

        public async Task AddAsync(Income income)
        {
            await _context.Incomes.AddAsync(income);
        }

        public async Task UpdateAsync(Income income)
        {
            _context.Incomes.Update(income);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var income = await _context.Incomes.FindAsync(id);

            if (income != null)
            {
                _context.Incomes.Remove(income);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}