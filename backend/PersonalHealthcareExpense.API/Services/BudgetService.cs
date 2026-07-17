using AutoMapper;
using PersonalHealthcareExpense.API.DTOs.Budget;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _repository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public BudgetService(
            IBudgetRepository repository,
            IExpenseRepository expenseRepository,
            IMapper mapper)
        {
            _repository = repository;
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BudgetDTO>> GetAllByUserIdAsync(int userId)
        {
            var budgets = await _repository.GetAllByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<BudgetDTO>>(budgets);
        }

        public async Task<BudgetDTO?> GetByIdAsync(int id)
        {
            var budget = await _repository.GetByIdAsync(id);

            if (budget == null)
                return null;

            return _mapper.Map<BudgetDTO>(budget);
        }

        public async Task<object?> CheckBudgetAsync(int userId, string category, int month, int year)
        {
            var budget = await _repository.GetByCategoryMonthYearAsync(userId, category, month, year);

            if (budget == null)
                return null;

            var expenses = await _expenseRepository.GetAllByUserIdAsync(userId);

            var spent = expenses
                .Where(e => e.Category == category
                    && e.ExpenseDate.Month == month
                    && e.ExpenseDate.Year == year)
                .Sum(e => e.Amount);

            return new
            {
                overBudget = spent > budget.MonthlyLimit,
                spent = spent,
                limit = budget.MonthlyLimit
            };
        }

        public async Task AddAsync(int userId, AddBudgetDTO dto)
        {
            var budget = _mapper.Map<Budget>(dto);

            budget.UserId = userId;
            budget.CreatedDate = DateTime.Now;

            await _repository.AddAsync(budget);
            await _repository.SaveAsync();
        }

        public async Task<bool> UpdateAsync(int id, UpdateBudgetDTO dto)
        {
            var budget = await _repository.GetByIdAsync(id);

            if (budget == null)
                return false;

            budget.Category = dto.Category;
            budget.MonthlyLimit = dto.MonthlyLimit;
            budget.Month = dto.Month;
            budget.Year = dto.Year;

            await _repository.UpdateAsync(budget);
            await _repository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var budget = await _repository.GetByIdAsync(id);

            if (budget == null)
                return false;

            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();

            return true;
        }
    }
}
