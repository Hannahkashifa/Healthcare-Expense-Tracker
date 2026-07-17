namespace PersonalHealthcareExpense.API.DTOs.Healthcare
{
    public class HealthcareDTO
    {
        public int HealthcareId { get; set; }

        public int UserId { get; set; }

        public string HospitalName { get; set; } = string.Empty;

        public string DoctorName { get; set; } = string.Empty;

        public DateTime VisitDate { get; set; }

        public string Diagnosis { get; set; } = string.Empty;

        public decimal ConsultationFee { get; set; }

        public string? Notes { get; set; }
    }
}