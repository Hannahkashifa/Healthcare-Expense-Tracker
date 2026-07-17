namespace PersonalHealthcareExpense.API.DTOs.Expense
{
    public class ExpenseDTO
    {
        public int ExpenseId { get; set; }

        public string Category { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime ExpenseDate { get; set; }

        public string? Description { get; set; }
    }
}