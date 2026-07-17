using Microsoft.EntityFrameworkCore;
using PersonalHealthcareExpense.API.Data;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Repository
{
    public class HealthcareRepository : IHealthcareRepository
    {
        private readonly ApplicationDbContext _context;

        public HealthcareRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Healthcare>> GetAllAsync()
        {
            return await _context.Healthcares.ToListAsync();
        }

        public async Task<IEnumerable<Healthcare>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Healthcares.Where(h => h.UserId == userId).ToListAsync();
        }

        public async Task<Healthcare?> GetByIdAsync(int id)
        {
            return await _context.Healthcares.FindAsync(id);
        }

        public async Task AddAsync(Healthcare healthcare)
        {
            await _context.Healthcares.AddAsync(healthcare);
        }

        public async Task UpdateAsync(Healthcare healthcare)
        {
            _context.Healthcares.Update(healthcare);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var healthcare = await _context.Healthcares.FindAsync(id);

            if (healthcare != null)
            {
                _context.Healthcares.Remove(healthcare);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}