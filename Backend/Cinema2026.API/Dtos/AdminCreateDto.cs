namespace Cinema2026.API.Dtos
{
    // Bruges når man OPRETTER en ny admin (POST).
    public class AdminCreateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}