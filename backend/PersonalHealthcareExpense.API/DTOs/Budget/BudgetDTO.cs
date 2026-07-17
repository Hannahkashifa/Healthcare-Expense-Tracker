namespace PersonalHealthcareExpense.API.DTOs.Budget
{
    public class BudgetDTO
    {
        public int BudgetId { get; set; }

        public string Category { get; set; } = string.Empty;

        public decimal MonthlyLimit { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
