namespace PersonalHealthcareExpense.API.DTOs.User
{
    public class JwtResponseDTO
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }
    }
}