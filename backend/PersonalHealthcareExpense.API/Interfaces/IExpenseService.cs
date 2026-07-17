using PersonalHealthcareExpense.API.DTOs.Expense;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDTO>> GetAllExpensesAsync();
        Task<IEnumerable<ExpenseDTO>> GetAllByUserIdAsync(int userId);

        Task<ExpenseDTO?> GetExpenseByIdAsync(int id);

        Task<bool> AddExpenseAsync(int userId, AddExpenseDTO dto);

        Task<bool> UpdateExpenseAsync(int id, UpdateExpenseDTO dto);

        Task<IEnumerable<ExpenseDTO>> SearchExpensesAsync(string keyword);

        Task<IEnumerable<ExpenseDTO>> FilterExpensesByCategoryAsync(string category);
        Task<bool> DeleteExpenseAsync(int id);
    }
}