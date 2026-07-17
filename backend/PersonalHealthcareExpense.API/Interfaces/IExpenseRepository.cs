using PersonalHealthcareExpense.API.DTOs.Expense;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<IEnumerable<Expense>> GetAllByUserIdAsync(int userId);

        Task<Expense?> GetExpenseByIdAsync(int id);

        Task AddExpenseAsync(Expense expense);

        Task UpdateExpenseAsync(Expense expense);

        Task DeleteExpenseAsync(int id);

        Task<IEnumerable<Expense>> SearchExpensesAsync(string keyword);

        Task<IEnumerable<Expense>> FilterExpensesByCategoryAsync(string category);

        //Task<ExpenseSummaryDTO> GetExpenseSummaryAsync(int userId);
        Task SaveAsync();
    }
}