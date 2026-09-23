namespace Cinema2026.API.Dtos
{
    public class MovieHallReadDto
    {
        public int MovieHallId { get; set; }   
        public bool MovieHallOccupied { get; set; }
        public int? MovieId { get; set; }
        public List<int> PersonIds { get; set; } = new ();
    }
}
