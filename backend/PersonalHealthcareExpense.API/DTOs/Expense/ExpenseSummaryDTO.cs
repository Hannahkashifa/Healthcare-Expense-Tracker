namespace PersonalHealthcareExpense.API.DTOs.Expense
{
    public class ExpenseSummaryDTO
    {
        public decimal TotalExpense { get; set; }

        public decimal MonthlyExpense { get; set; }

        public decimal TodayExpense { get; set; }
    }
}