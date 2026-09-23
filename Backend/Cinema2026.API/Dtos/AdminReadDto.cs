namespace Cinema2026.API.Dtos
{
    // Bruges når vi SENDER data tilbage til klienten.
    // Bemærk: Password er IKKE med her - den skal aldrig sendes tilbage til klienten.
    public class AdminReadDto
    {
        public int AdminId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}