using System.ComponentModel.DataAnnotations;

namespace PersonalHealthcareExpense.API.DTOs.Income
{
    public class UpdateIncomeDTO
    {
        [Required]
        public string Source { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime IncomeDate { get; set; }

        public string? Description { get; set; }
    }
}