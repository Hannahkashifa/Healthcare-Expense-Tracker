using BCrypt.Net;
using PersonalHealthcareExpense.API.DTOs.User;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtService _jwtService;

        public UserService(
            IUserRepository repository,
            IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDTO dto)
        {
            var existingUser = await _repository.GetUserByEmailAsync(dto.Email);

            if (existingUser != null)
                return false;

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                CreatedDate = DateTime.Now
            };

            await _repository.AddUserAsync(user);

            await _repository.SaveAsync();

            return true;
        }

        public async Task<string?> LoginAsync(LoginDTO dto)
        {
            var user = await _repository.GetUserByEmailAsync(dto.Email);

            if (user == null)
                return null;

            bool verified = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!verified)
                return null;

            return _jwtService.GenerateToken(user);
        }
    }
}