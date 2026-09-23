namespace Cinema2026.API.Dtos
{
    // Bruges når klienten OPDATERER en eksisterende person (PUT).
    // PersonId kommer fra URL'en (route), ikke fra body - derfor ikke med her.
    public class PersonUpdateDto
    {
        public string name { get; set; }
        public int age { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? MovieHallId { get; set; }
    }

}
