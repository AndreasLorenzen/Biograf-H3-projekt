namespace Cinema2026.API.Dtos
{
    // Bruges når klienten OPDATERER en eksisterende film (PUT).
    // MovieId kommer fra URL'en (route), ikke fra body.
    public class MovieUpdateDto
    {
        public string Moviename { get; set; }
        public int Movieage { get; set; }
    }
}