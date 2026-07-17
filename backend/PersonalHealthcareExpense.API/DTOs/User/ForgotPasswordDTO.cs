using System.ComponentModel.DataAnnotations;

namespace PersonalHealthcareExpense.API.DTOs.User
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
