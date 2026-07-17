using Microsoft.EntityFrameworkCore;
using PersonalHealthcareExpense.API.Data;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Repository
{
    public class PasswordResetTokenRepository : IPasswordResetTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public PasswordResetTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PasswordResetToken?> GetValidTokenAsync(string token)
        {
            return await _context.PasswordResetTokens
                .FirstOrDefaultAsync(t => t.Token == token && !t.IsUsed && t.ExpiresAt > DateTime.UtcNow);
        }

        public async Task<PasswordResetToken?> GetByUserIdAsync(int userId)
        {
            return await _context.PasswordResetTokens
                .Where(t => t.UserId == userId && !t.IsUsed)
                .OrderByDescending(t => t.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(PasswordResetToken resetToken)
        {
            _context.PasswordResetTokens.Add(resetToken);
            await _context.SaveChangesAsync();
        }

        public async Task MarkUsedAsync(PasswordResetToken resetToken)
        {
            resetToken.IsUsed = true;
            await _context.SaveChangesAsync();
        }

        public async Task InvalidateOldTokensAsync(int userId)
        {
            var tokens = await _context.PasswordResetTokens
                .Where(t => t.UserId == userId && !t.IsUsed)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.IsUsed = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
