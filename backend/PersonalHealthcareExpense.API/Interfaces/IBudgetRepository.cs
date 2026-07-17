using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> GetAllByUserIdAsync(int userId);

        Task<Budget?> GetByIdAsync(int id);

        Task<Budget?> GetByCategoryMonthYearAsync(int userId, string category, int month, int year);

        Task AddAsync(Budget budget);

        Task UpdateAsync(Budget budget);

        Task DeleteAsync(int id);

        Task SaveAsync();
    }
}
