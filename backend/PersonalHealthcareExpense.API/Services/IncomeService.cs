using AutoMapper;
using PersonalHealthcareExpense.API.DTOs.Income;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _repository;
        private readonly IMapper _mapper;

        public IncomeService(IIncomeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IncomeDTO>> GetAllAsync()
        {
            var incomes = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<IncomeDTO>>(incomes);
        }

        public async Task<IEnumerable<IncomeDTO>> GetAllByUserIdAsync(int userId)
        {
            var incomes = await _repository.GetAllByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<IncomeDTO>>(incomes);
        }

        public async Task<IncomeDTO?> GetByIdAsync(int id)
        {
            var income = await _repository.GetByIdAsync(id);

            if (income == null)
                return null;

            return _mapper.Map<IncomeDTO>(income);
        }

        public async Task AddAsync(int userId, AddIncomeDTO dto)
        {
            var income = _mapper.Map<Income>(dto);

            income.UserId = userId;

            await _repository.AddAsync(income);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(int id, UpdateIncomeDTO dto)
        {
            var income = await _repository.GetByIdAsync(id);

            if (income == null)
                return;

            _mapper.Map(dto, income);

            await _repository.UpdateAsync(income);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }
    }
}