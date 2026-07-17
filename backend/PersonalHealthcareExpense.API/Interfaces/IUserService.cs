using PersonalHealthcareExpense.API.DTOs.User;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserDTO dto);

        Task<string?> LoginAsync(LoginDTO dto);
    }
}