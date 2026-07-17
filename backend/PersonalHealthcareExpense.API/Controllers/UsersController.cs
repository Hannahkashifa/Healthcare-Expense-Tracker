using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonalHealthcareExpense.API.Data;
using PersonalHealthcareExpense.API.DTOs;
using PersonalHealthcareExpense.API.DTOs.User;
using PersonalHealthcareExpense.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PersonalHealthcareExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public UsersController(
            IUserRepository repository,
            IUserService userService,
            IMapper mapper,
            ApplicationDbContext context)
        {
            _repository = repository;
            _userService = userService;
            _mapper = mapper;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.GetAllUsersAsync();
            var result = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(result);
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var success = await _userService.RegisterUserAsync(dto);
            if (!success)
                return BadRequest("Email already exists.");
            return Ok("User registered successfully.");
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var token = await _userService.LoginAsync(dto);
            if (token == null)
                return Unauthorized("Invalid email or password.");
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(new { UserId = userId, Name = name, Email = email });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = await _repository.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");
            var result = _mapper.Map<UserDTO>(user);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = await _repository.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");
            user.FullName = dto.FullName;
            user.PhoneNumber = dto.PhoneNumber;
            user.DateOfBirth = dto.DateOfBirth;
            user.Gender = dto.Gender;
            await _repository.UpdateUserAsync(user);
            await _repository.SaveAsync();
            return Ok("Profile updated successfully.");
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = await _repository.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
                return BadRequest("Current password is incorrect.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _repository.UpdateUserAsync(user);
            await _repository.SaveAsync();

            return Ok("Password changed successfully.");
        }

        [Authorize]
        [HttpGet("export")]
        public async Task<IActionResult> ExportData()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = await _repository.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");
            var expenses = _context.Expenses.Where(e => e.UserId == userId).ToList();
            var incomes = _context.Incomes.Where(i => i.UserId == userId).ToList();
            var healthcares = _context.Healthcares.Where(h => h.UserId == userId).ToList();
            var healthcareIds = healthcares.Select(h => h.HealthcareId).ToList();
            var medicines = _context.Medicines.Where(m => healthcareIds.Contains(m.HealthcareId)).ToList();
            var budgets = _context.Budgets.Where(b => b.UserId == userId).ToList();
            return Ok(new
            {
                Profile = new { user.UserId, user.FullName, user.Email, user.PhoneNumber, user.DateOfBirth, user.Gender, user.CreatedDate },
                Expenses = expenses,
                Incomes = incomes,
                Healthcares = healthcares,
                Medicines = medicines,
                Budgets = budgets
            });
        }
    }
}