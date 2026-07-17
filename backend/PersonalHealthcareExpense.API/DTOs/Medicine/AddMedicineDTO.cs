using System.ComponentModel.DataAnnotations;

namespace PersonalHealthcareExpense.API.DTOs.Medicine
{
    public class AddMedicineDTO
    {
        [Required]
        public int HealthcareId { get; set; }

        [Required]
        public string MedicineName { get; set; } = string.Empty;

        public int MorningDose { get; set; }

        public int AfternoonDose { get; set; }

        public int NightDose { get; set; }

        public int DurationInDays { get; set; }

        public decimal Price { get; set; }
    }
}