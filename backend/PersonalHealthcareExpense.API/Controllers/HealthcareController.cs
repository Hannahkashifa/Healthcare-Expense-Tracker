using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalHealthcareExpense.API.DTOs.Healthcare;
using PersonalHealthcareExpense.API.Interfaces;
using System.Security.Claims;

namespace PersonalHealthcareExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HealthcareController : ControllerBase
    {
        private readonly IHealthcareService _service;

        public HealthcareController(IHealthcareService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.GetAllByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddHealthcareDTO dto)
        {
            dto.UserId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            await _service.AddAsync(dto);

            return Ok("Healthcare record added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateHealthcareDTO dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok("Healthcare record updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok("Healthcare record deleted successfully.");
        }
    }
}