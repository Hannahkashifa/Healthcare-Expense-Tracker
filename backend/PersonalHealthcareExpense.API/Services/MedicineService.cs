using AutoMapper;
using PersonalHealthcareExpense.API.DTOs.Medicine;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _repository;
        private readonly IMapper _mapper;

        public MedicineService(
            IMedicineRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicineDTO>> GetAllAsync()
        {
            var medicines = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MedicineDTO>>(medicines);
        }

        public async Task<IEnumerable<MedicineDTO>> GetAllByUserIdAsync(int userId)
        {
            var medicines = await _repository.GetAllByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<MedicineDTO>>(medicines);
        }

        public async Task<MedicineDTO?> GetByIdAsync(int id)
        {
            var medicine = await _repository.GetByIdAsync(id);

            if (medicine == null)
                return null;

            return _mapper.Map<MedicineDTO>(medicine);
        }

        public async Task AddAsync(AddMedicineDTO dto)
        {
            var medicine = _mapper.Map<Medicine>(dto);

            await _repository.AddAsync(medicine);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(int id, UpdateMedicineDTO dto)
        {
            var medicine = await _repository.GetByIdAsync(id);

            if (medicine == null)
                return;

            _mapper.Map(dto, medicine);

            await _repository.UpdateAsync(medicine);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }
    }
}