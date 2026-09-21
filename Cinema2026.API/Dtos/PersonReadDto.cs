namespace Cinema2026.API.Dtos
{
    // Bruges når vi SENDER data tilbage til klienten (GET, POST-svar osv.).
    // Kun "flade" felter - ingen navigation properties, så ingen cirkulære referencer.
    public class PersonReadDto
    {
        public int PersonId { get; set; }
        public string Personname { get; set; }
        public int Personage { get; set; }
        public int? MovieHallId { get; set; }
    }
}
