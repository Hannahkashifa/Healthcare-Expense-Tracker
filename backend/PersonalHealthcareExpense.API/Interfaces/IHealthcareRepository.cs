using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IHealthcareRepository
    {
        Task<IEnumerable<Healthcare>> GetAllAsync();
        Task<IEnumerable<Healthcare>> GetAllByUserIdAsync(int userId);

        Task<Healthcare?> GetByIdAsync(int id);

        Task AddAsync(Healthcare healthcare);

        Task UpdateAsync(Healthcare healthcare);

        Task DeleteAsync(int id);

        Task SaveAsync();
    }
}