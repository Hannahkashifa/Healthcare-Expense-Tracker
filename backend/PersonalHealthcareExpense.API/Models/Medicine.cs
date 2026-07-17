using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalHealthcareExpense.API.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }

        public int HealthcareId { get; set; }

        [Required]
        public string MedicineName { get; set; } = string.Empty;

        public int MorningDose { get; set; }

        public int AfternoonDose { get; set; }

        public int NightDose { get; set; }

        public int DurationInDays { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("HealthcareId")]
        public Healthcare? Healthcare { get; set; }
    }
}