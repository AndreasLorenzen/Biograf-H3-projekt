namespace Cinema2026.API.Dtos
{
    // Bruges når man OPDATERER en eksisterende admin (PUT).
    public class AdminUpdateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}