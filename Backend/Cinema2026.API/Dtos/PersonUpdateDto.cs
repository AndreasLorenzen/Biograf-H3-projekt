namespace Cinema2026.API.Dtos
{
    // Bruges når klienten OPDATERER en eksisterende person (PUT).
    // PersonId kommer fra URL'en (route), ikke fra body - derfor ikke med her.
    public class PersonUpdateDto
    {
        public string Personname { get; set; }
        public int Personage { get; set; }
        public int? MovieHallId { get; set; }
    }

}
