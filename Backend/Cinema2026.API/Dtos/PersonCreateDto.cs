namespace Cinema2026.API.Dtos
{
    // Bruges når klienten OPRETTER en ny person (POST).
    // Ingen PersonId her - databasen genererer selv ID'et.
    public class PersonCreateDto
    {
        public string Personname { get; set; }
        public int Personage { get; set; }
        public int? MovieHallId { get; set; } // valgfrit: hvilken sal personen evt. sidder i
    }

}
