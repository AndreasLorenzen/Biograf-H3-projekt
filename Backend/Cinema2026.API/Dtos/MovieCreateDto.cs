namespace Cinema2026.API.Dtos
{
    // Bruges når klienten OPRETTER en ny film (POST).
    // Ingen MovieId her - databasen genererer selv ID'et.
    public class MovieCreateDto
    {
        public string Moviename { get; set; }
        public int Movieage { get; set; }
    }
}