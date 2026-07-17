using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IIncomeRepository
    {
        Task<IEnumerable<Income>> GetAllAsync();
        Task<IEnumerable<Income>> GetAllByUserIdAsync(int userId);

        Task<Income?> GetByIdAsync(int id);

        Task AddAsync(Income income);

        Task UpdateAsync(Income income);

        Task DeleteAsync(int id);

        Task SaveAsync();
    }
}