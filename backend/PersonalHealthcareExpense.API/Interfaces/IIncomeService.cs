using PersonalHealthcareExpense.API.DTOs.Income;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IIncomeService
    {
        Task<IEnumerable<IncomeDTO>> GetAllAsync();
        Task<IEnumerable<IncomeDTO>> GetAllByUserIdAsync(int userId);

        Task<IncomeDTO?> GetByIdAsync(int id);

        Task AddAsync(int userId, AddIncomeDTO dto);

        Task UpdateAsync(int id, UpdateIncomeDTO dto);

        Task DeleteAsync(int id);
    }
}