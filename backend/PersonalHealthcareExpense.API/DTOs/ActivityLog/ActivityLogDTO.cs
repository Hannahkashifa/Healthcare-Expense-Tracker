namespace PersonalHealthcareExpense.API.DTOs.ActivityLog
{
    public class ActivityLogDTO
    {
        public int ActivityLogId { get; set; }

        public string Action { get; set; } = string.Empty;

        public string? Details { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
