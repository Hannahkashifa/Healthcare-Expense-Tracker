using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalHealthcareExpense.API.DTOs.Budget;
using PersonalHealthcareExpense.API.Interfaces;
using System.Security.Claims;

namespace PersonalHealthcareExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var budgets = await _budgetService.GetAllByUserIdAsync(userId);
            return Ok(budgets);
        }

        [HttpGet("check/{category}/{month}/{year}")]
        public async Task<IActionResult> CheckBudget(string category, int month, int year)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _budgetService.CheckBudgetAsync(userId, category, month, year);

            if (result == null)
                return NotFound("No budget found for this category and month.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBudgetDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _budgetService.AddAsync(userId, dto);

            return Ok("Budget added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBudgetDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _budgetService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound("Budget not found.");

            return Ok("Budget updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _budgetService.DeleteAsync(id);

            if (!deleted)
                return NotFound("Budget not found.");

            return Ok("Budget deleted successfully.");
        }
    }
}
