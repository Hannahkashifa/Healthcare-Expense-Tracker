using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonalHealthcareExpense.API.Data;
using PersonalHealthcareExpense.API.DTOs;
using PersonalHealthcareExpense.API.DTOs.User;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;
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

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _repository.GetUserByEmailAsync(dto.Email);
            if (user == null)
            {
                return Ok("If the email exists, a reset link has been sent.");
            }

            var tokenRepo = HttpContext.RequestServices.GetRequiredService<IPasswordResetTokenRepository>();
            var emailService = HttpContext.RequestServices.GetRequiredService<IEmailService>();

            await tokenRepo.InvalidateOldTokensAsync(user.UserId);

            var resetToken = new PasswordResetToken
            {
                UserId = user.UserId,
                Token = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N"),
                ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            await tokenRepo.CreateAsync(resetToken);

            var resetLink = $"https://localhost:7080/Account/ResetPassword?token={resetToken.Token}";

            var htmlBody = $@"
                <div style='font-family: -apple-system, BlinkMacSystemFont, sans-serif; max-width: 500px; margin: 0 auto; padding: 40px;'>
                    <div style='text-align: center; margin-bottom: 30px;'>
                        <h1 style='color: #1d1d1f; font-size: 24px; margin-bottom: 8px;'>Healthcare Expense Tracker</h1>
                    </div>
                    <div style='background: #f5f5f7; border-radius: 16px; padding: 32px;'>
                        <h2 style='color: #1d1d1f; font-size: 20px; margin-bottom: 12px;'>Reset Your Password</h2>
                        <p style='color: #6e6e73; font-size: 15px; line-height: 1.6;'>
                            Hi {user.FullName}, we received a request to reset your password.
                        </p>
                        <p style='color: #6e6e73; font-size: 15px; line-height: 1.6;'>
                            Click the button below to set a new password. This link expires in <strong>15 minutes</strong>.
                        </p>
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='{resetLink}' style='display: inline-block; padding: 14px 32px; background: #0071e3; color: #fff; text-decoration: none; border-radius: 10px; font-weight: 600; font-size: 15px;'>
                                Reset Password
                            </a>
                        </div>
                        <p style='color: #aeaeb2; font-size: 13px; line-height: 1.5;'>
                            If you didn't request this, you can safely ignore this email.
                        </p>
                    </div>
                    <p style='color: #aeaeb2; font-size: 12px; text-align: center; margin-top: 24px;'>
                        Healthcare Expense Tracker &mdash; Secure Health &amp; Finance Management
                    </p>
                </div>";

            await emailService.SendAsync(user.Email, "Reset Your Password - Healthcare Tracker", htmlBody);

            return Ok("If the email exists, a reset link has been sent.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tokenRepo = HttpContext.RequestServices.GetRequiredService<IPasswordResetTokenRepository>();

            var resetToken = await tokenRepo.GetValidTokenAsync(dto.Token);
            if (resetToken == null)
                return BadRequest("Invalid or expired reset token.");

            var user = await _repository.GetUserByIdAsync(resetToken.UserId);
            if (user == null)
                return BadRequest("User not found.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _repository.UpdateUserAsync(user);
            await _repository.SaveAsync();

            await tokenRepo.MarkUsedAsync(resetToken);

            return Ok("Password has been reset successfully.");
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