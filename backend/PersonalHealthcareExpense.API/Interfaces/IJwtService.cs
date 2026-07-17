using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}