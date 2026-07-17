using System.ComponentModel.DataAnnotations;

namespace PersonalHealthcareExpense.API.DTOs.Budget
{
    public class AddBudgetDTO
    {
        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public decimal MonthlyLimit { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }
    }
}
