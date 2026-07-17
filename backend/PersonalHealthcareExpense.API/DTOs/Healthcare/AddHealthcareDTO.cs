using System.ComponentModel.DataAnnotations;

namespace PersonalHealthcareExpense.API.DTOs.Healthcare
{
    public class AddHealthcareDTO
    {
        [Required]
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
    }
}