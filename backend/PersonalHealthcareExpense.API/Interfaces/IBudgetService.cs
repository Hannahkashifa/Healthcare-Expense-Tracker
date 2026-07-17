using PersonalHealthcareExpense.API.DTOs.Budget;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<BudgetDTO>> GetAllByUserIdAsync(int userId);

        Task<BudgetDTO?> GetByIdAsync(int id);

        Task<object?> CheckBudgetAsync(int userId, string category, int month, int year);

        Task AddAsync(int userId, AddBudgetDTO dto);

        Task<bool> UpdateAsync(int id, UpdateBudgetDTO dto);

        Task<bool> DeleteAsync(int id);
    }
}
