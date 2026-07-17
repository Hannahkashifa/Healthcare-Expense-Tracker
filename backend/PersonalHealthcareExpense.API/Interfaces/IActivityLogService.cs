using PersonalHealthcareExpense.API.DTOs.ActivityLog;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IActivityLogService
    {
        Task<IEnumerable<ActivityLogDTO>> GetLastByUserIdAsync(int userId, int count);

        Task LogAsync(int userId, string action, string? details);
    }
}
