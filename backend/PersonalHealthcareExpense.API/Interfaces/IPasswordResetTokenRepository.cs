using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IPasswordResetTokenRepository
    {
        Task<PasswordResetToken?> GetValidTokenAsync(string token);
        Task<PasswordResetToken?> GetByUserIdAsync(int userId);
        Task CreateAsync(PasswordResetToken resetToken);
        Task MarkUsedAsync(PasswordResetToken resetToken);
        Task InvalidateOldTokensAsync(int userId);
    }
}
