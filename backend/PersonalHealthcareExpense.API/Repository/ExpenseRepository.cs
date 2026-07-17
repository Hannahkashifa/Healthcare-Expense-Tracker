using Microsoft.EntityFrameworkCore;
using PersonalHealthcareExpense.API.Data;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;
using System.Linq;

namespace PersonalHealthcareExpense.API.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Expenses.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task<Expense?> GetExpenseByIdAsync(int id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await Task.CompletedTask;
        }

        public async Task DeleteExpenseAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense != null)
            {
                _context.Expenses.Remove(expense);
            }
        }
        public async Task<IEnumerable<Expense>> SearchExpensesAsync(string keyword)
        {
            return await _context.Expenses
                .Where(e =>
                    e.Category.Contains(keyword) ||
                    (e.Description != null && e.Description.Contains(keyword)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> FilterExpensesByCategoryAsync(string category)
        {
            return await _context.Expenses
                .Where(e => e.Category == category)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}