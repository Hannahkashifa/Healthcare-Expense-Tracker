using PersonalHealthcareExpense.API.DTOs.Dashboard;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetDashboardAsync(int userId);
    }
}