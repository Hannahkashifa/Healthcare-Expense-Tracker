using System.ComponentModel.DataAnnotations;

namespace PersonalHealthcareExpense.API.DTOs.User
{
    public class UpdateProfileDTO
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }
    }
}
