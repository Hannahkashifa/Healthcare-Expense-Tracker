using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalHealthcareExpense.API.Models
{
    public class ActivityLog
    {
        [Key]
        public int ActivityLogId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        public string Action { get; set; } = string.Empty;

        public string? Details { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
