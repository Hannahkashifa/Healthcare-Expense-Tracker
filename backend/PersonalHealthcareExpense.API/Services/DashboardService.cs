using Microsoft.EntityFrameworkCore;
using PersonalHealthcareExpense.API.Data;
using PersonalHealthcareExpense.API.DTOs.Dashboard;
using PersonalHealthcareExpense.API.Interfaces;

namespace PersonalHealthcareExpense.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDTO> GetDashboardAsync(int userId)
        {
            var totalIncome = await _context.Incomes
                .Where(i => i.UserId == userId)
                .SumAsync(i => (decimal?)i.Amount) ?? 0;

            var totalExpense = await _context.Expenses
                .Where(e => e.UserId == userId)
                .SumAsync(e => (decimal?)e.Amount) ?? 0;

            var incomeCount = await _context.Incomes
                .CountAsync(i => i.UserId == userId);

            var expenseCount = await _context.Expenses
                .CountAsync(e => e.UserId == userId);

            var healthcareExpense = await _context.Healthcares
    .Where(h => h.UserId == userId)
    .SumAsync(h => (decimal?)h.ConsultationFee) ?? 0;

            var healthcareCount = await _context.Healthcares
                .CountAsync(h => h.UserId == userId);

            var medicineCount = await _context.Medicines
                .CountAsync();

            return new DashboardDTO
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                TotalHealthcareExpense = healthcareExpense,
                CurrentBalance = totalIncome - totalExpense,
                TotalIncomeTransactions = incomeCount,
                TotalExpenseTransactions = expenseCount,
                TotalHealthcareVisits = healthcareCount,
                TotalMedicines = medicineCount
            };
        }
    }
}