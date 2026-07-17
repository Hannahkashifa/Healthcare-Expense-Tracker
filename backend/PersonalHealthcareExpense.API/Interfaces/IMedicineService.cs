using PersonalHealthcareExpense.API.DTOs.Medicine;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IMedicineService
    {
        Task<IEnumerable<MedicineDTO>> GetAllAsync();
        Task<IEnumerable<MedicineDTO>> GetAllByUserIdAsync(int userId);

        Task<MedicineDTO?> GetByIdAsync(int id);

        Task AddAsync(AddMedicineDTO dto);

        Task UpdateAsync(int id, UpdateMedicineDTO dto);

        Task DeleteAsync(int id);
    }
}