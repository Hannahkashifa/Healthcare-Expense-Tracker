using Microsoft.EntityFrameworkCore;
using PersonalHealthcareExpense.API.Data;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Repository
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ActivityLog>> GetLastByUserIdAsync(int userId, int count)
        {
            return await _context.ActivityLogs
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Timestamp)
                .Take(count)
                .ToListAsync();
        }

        public async Task AddAsync(ActivityLog activityLog)
        {
            await _context.ActivityLogs.AddAsync(activityLog);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
