namespace Cinema2026.API.Dtos
{
    // Bruges når klienten OPRETTER en ny person (POST).
    // Ingen PersonId her - databasen genererer selv ID'et.
    public class PersonCreateDto
    {
        public string name { get; set; }
        public int age { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? MovieHallId { get; set; } // valgfrit: hvilken sal personen evt. sidder i
    }

}
