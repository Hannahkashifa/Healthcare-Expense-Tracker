using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IActivityLogRepository
    {
        Task<IEnumerable<ActivityLog>> GetLastByUserIdAsync(int userId, int count);

        Task AddAsync(ActivityLog activityLog);

        Task SaveAsync();
    }
}
