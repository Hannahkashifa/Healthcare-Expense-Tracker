using PersonalHealthcareExpense.API.DTOs.Healthcare;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IHealthcareService
    {
        Task<IEnumerable<HealthcareDTO>> GetAllAsync();
        Task<IEnumerable<HealthcareDTO>> GetAllByUserIdAsync(int userId);

        Task<HealthcareDTO?> GetByIdAsync(int id);

        Task AddAsync(AddHealthcareDTO dto);

        Task UpdateAsync(int id, UpdateHealthcareDTO dto);

        Task DeleteAsync(int id);
    }
}