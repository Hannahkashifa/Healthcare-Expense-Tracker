using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> GetAllAsync();
        Task<IEnumerable<Medicine>> GetAllByUserIdAsync(int userId);

        Task<Medicine?> GetByIdAsync(int id);

        Task AddAsync(Medicine medicine);

        Task UpdateAsync(Medicine medicine);

        Task DeleteAsync(int id);

        Task SaveAsync();
    }
}