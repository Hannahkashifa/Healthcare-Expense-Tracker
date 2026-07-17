using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalHealthcareExpense.API.DTOs.Expense;
using PersonalHealthcareExpense.API.Interfaces;
using System.Security.Claims;
using System.Text.Json;
using System.Text;

namespace PersonalHealthcareExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // GET: api/Expense
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            int userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var expenses = await _expenseService.GetAllByUserIdAsync(userId);
            return Ok(expenses);
        }

        // GET: api/Expense/search?keyword=Food
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest("Keyword is required.");

            var result = await _expenseService.SearchExpensesAsync(keyword);

            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterByCategory([FromQuery] string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return BadRequest("Category is required.");

            var result = await _expenseService.FilterExpensesByCategoryAsync(category);

            return Ok(result);
        }
        // GET: api/Expense/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);

            if (expense == null)
                return NotFound("Expense not found.");

            return Ok(expense);
        }

        // POST: api/Expense
        [HttpPost]
        public async Task<IActionResult> AddExpense(AddExpenseDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            await _expenseService.AddExpenseAsync(userId, dto);

            return Ok("Expense added successfully.");
        }
        // PUT: api/Expense/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, UpdateExpenseDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _expenseService.UpdateExpenseAsync(id, dto);

            if (!updated)
                return NotFound("Expense not found.");

            return Ok("Expense updated successfully.");
        }

        // DELETE: api/Expense/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var deleted = await _expenseService.DeleteExpenseAsync(id);

            if (!deleted)
                return NotFound("Expense not found.");

            return Ok("Expense deleted successfully.");
        }
    }
}