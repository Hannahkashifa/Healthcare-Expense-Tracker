using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalHealthcareExpense.API.Models
{
    public class Healthcare
    {
        [Key]
        public int HealthcareId { get; set; }

        public int UserId { get; set; }

        [Required]
        public string HospitalName { get; set; } = string.Empty;

        [Required]
        public string DoctorName { get; set; } = string.Empty;

        [Required]
        public DateTime VisitDate { get; set; }

        [Required]
        public string Diagnosis { get; set; } = string.Empty;

        public decimal ConsultationFee { get; set; }

        public string? Notes { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<Medicine>? Medicines { get; set; }
    }
}