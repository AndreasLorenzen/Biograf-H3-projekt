using Cinema2026.API.Dtos;
using System.Linq;
using Cinema2026.Repo.Interfaces;
using Cinema2026.Repo.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")] // https://localhost:7073/api/moviehalls
[ApiController]
public class MovieHallsController : ControllerBase
{
    private readonly IMovieHallRepositories _repo;

    public MovieHallsController(IMovieHallRepositories repo)
    {
        _repo = repo;
    }

    // Lille hjælpe-metode: oversætter en MovieHall (model) til en MovieHallReadDto
    private MovieHallReadDto ToReadDto(MovieHall m)
    {
        return new MovieHallReadDto
        {
            MovieHallId = m.MovieHallId,
            MovieHallOccupied = m.MovieHalloccupied,
            MovieId = m.MovieId,
            // .Select() trækker kun PersonId ud af hver Person i listen
            PersonIds = m.Persons?.Select(p => p.PersonId).ToList() ?? new List<int>()
        };
    }

    // GET: api/MovieHalls
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieHallReadDto>>> GetMovieHall()
    {
        var moviehalls = await _repo.GetMovieHall();

        var result = moviehalls.Select(ToReadDto);

        return Ok(result);
    }

    // GET: api/MovieHalls/5
    [HttpGet("{moviehallid}")]
    public async Task<ActionResult<MovieHallReadDto>> GetMovieHall(int moviehallid)
    {
        var moviehall = await _repo.GetMovieHall(moviehallid);

        if (moviehall == null)
        {
            return NotFound();
        }

        return ToReadDto(moviehall);
    }

    // PUT: api/MovieHalls/5
    [HttpPut("{moviehallid}")]
    public async Task<IActionResult> PutMovieHall(int? moviehallid, MovieHallUpdateDto dto)
    {
        // Byg en MovieHall-model ud fra ID (fra URL) + DTO (fra body)
        var moviehall = new MovieHall
        {
            MovieHallId = moviehallid ?? 0,
            MovieHalloccupied = dto.MovieHallOccupied,
            MovieId = dto.MovieId
        };

        var success = await _repo.PutMovieHall(moviehallid, moviehall);
        if (!success) return BadRequest();

        return NoContent();
    }

    // POST: api/MovieHalls
    [HttpPost]
    public async Task<ActionResult<MovieHallReadDto>> PostMovieHall(MovieHallCreateDto dto)
    {
        // Byg en "rigtig" MovieHall ud fra DTO'en - MovieHallId sættes ikke,
        // databasen genererer det selv
        var moviehall = new MovieHall
        {
            MovieHalloccupied = dto.MovieHallOccupied,
            MovieId = dto.MovieId
        };

        var created = await _repo.PostMovieHall(moviehall);

        // Returnér den oprettede sal som en ReadDto, med 201 Created
        return CreatedAtAction(nameof(GetMovieHall), new { moviehallid = created.MovieHallId }, ToReadDto(created));
    }

    // DELETE: api/MovieHalls/5
    [HttpDelete("{moviehallid}")]
    public async Task<IActionResult> DeleteMovieHall(int? moviehallid)
    {
        var success = await _repo.DeleteMovieHall(moviehallid);
        if (!success) return NotFound();

        return NoContent();
    }
}