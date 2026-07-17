using AutoMapper;
using PersonalHealthcareExpense.API.DTOs.Expense;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllExpensesAsync()
        {
            var expenses = await _repository.GetAllExpensesAsync();
            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllByUserIdAsync(int userId)
        {
            var expenses = await _repository.GetAllByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
        }

        public async Task<ExpenseDTO?> GetExpenseByIdAsync(int id)
        {
            var expense = await _repository.GetExpenseByIdAsync(id);

            if (expense == null)
                return null;

            return _mapper.Map<ExpenseDTO>(expense);
        }

        public async Task<bool> AddExpenseAsync(int userId, AddExpenseDTO dto)
        {
            var expense = _mapper.Map<Expense>(dto);

            // TEMPORARY TEST
            expense.UserId = userId;

            expense.CreatedDate = DateTime.Now;

            await _repository.AddExpenseAsync(expense);
            await _repository.SaveAsync();

            return true;
        }

        public async Task<bool> UpdateExpenseAsync(int id, UpdateExpenseDTO dto)
        {
            var expense = await _repository.GetExpenseByIdAsync(id);

            if (expense == null)
                return false;

            expense.Category = dto.Category;
            expense.Amount = dto.Amount;
            expense.ExpenseDate = dto.ExpenseDate;
            expense.Description = dto.Description;
            expense.IsRecurring = dto.IsRecurring;

            await _repository.UpdateExpenseAsync(expense);
            await _repository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            var expense = await _repository.GetExpenseByIdAsync(id);

            if (expense == null)
                return false;

            await _repository.DeleteExpenseAsync(id);
            await _repository.SaveAsync();

            return true;
        }

        public async Task<IEnumerable<ExpenseDTO>> SearchExpensesAsync(string keyword)
        {
            var expenses = await _repository.SearchExpensesAsync(keyword);
            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
        }

        public async Task<IEnumerable<ExpenseDTO>> FilterExpensesByCategoryAsync(string category)
        {
            var expenses = await _repository.FilterExpensesByCategoryAsync(category);
            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
        }
    }
}