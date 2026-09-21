using System.Collections.Generic;

namespace Cinema2026.API.Dtos
{
    // Bruges når vi SENDER data tilbage til klienten.
    // MovieHallIds i stedet for hele MovieHall-objekter, så vi undgår cirkler.
    public class MovieReadDto
    {
        public int MovieId { get; set; }
        public string Moviename { get; set; }
        public int Movieage { get; set; }

        // ID'er på de sale, der lige nu viser denne film
        public List<int> MovieHallIds { get; set; } = new();
    }
}