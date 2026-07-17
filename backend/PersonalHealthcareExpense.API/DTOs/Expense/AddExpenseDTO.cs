using System.ComponentModel.DataAnnotations;

namespace PersonalHealthcareExpense.API.DTOs.Expense
{
    public class AddExpenseDTO
    {
        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }

        public string? Description { get; set; }

        public bool IsRecurring { get; set; }
    }
}