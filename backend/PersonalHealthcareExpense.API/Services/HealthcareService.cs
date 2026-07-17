using AutoMapper;
using PersonalHealthcareExpense.API.DTOs.Healthcare;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Services
{
    public class HealthcareService : IHealthcareService
    {
        private readonly IHealthcareRepository _repository;
        private readonly IMapper _mapper;

        public HealthcareService(
            IHealthcareRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HealthcareDTO>> GetAllAsync()
        {
            var healthcares = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<HealthcareDTO>>(healthcares);
        }

        public async Task<IEnumerable<HealthcareDTO>> GetAllByUserIdAsync(int userId)
        {
            var healthcares = await _repository.GetAllByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<HealthcareDTO>>(healthcares);
        }

        public async Task<HealthcareDTO?> GetByIdAsync(int id)
        {
            var healthcare = await _repository.GetByIdAsync(id);

            if (healthcare == null)
                return null;

            return _mapper.Map<HealthcareDTO>(healthcare);
        }

        public async Task AddAsync(AddHealthcareDTO dto)
        {
            var healthcare = _mapper.Map<Healthcare>(dto);

            await _repository.AddAsync(healthcare);

            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(int id, UpdateHealthcareDTO dto)
        {
            var healthcare = await _repository.GetByIdAsync(id);

            if (healthcare == null)
                return;

            _mapper.Map(dto, healthcare);

            await _repository.UpdateAsync(healthcare);

            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);

            await _repository.SaveAsync();
        }
    }
}