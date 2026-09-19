using Microsoft.AspNetCore.Mvc;
using Cinema2026.Repo.Models;
using Cinema2026.Repo.Interfaces;

[Route("api/[controller]")] // https://localhost:7073/api/moviehalls
[ApiController]
public class MovieHallsController : ControllerBase
{
    private readonly IMovieHallRepositories _repo;

    public MovieHallsController(IMovieHallRepositories repo)
    {
        _repo = repo;
    }

    // GET: api/MovieHalls
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieHall>>> GetMovieHall()
    {
        return Ok(await _repo.GetMovieHall());
    }

    // GET: api/MovieHalls/5
    [HttpGet("{moviehallid}")]
    public async Task<ActionResult<MovieHall>> GetMovieHall(int moviehallid)
    {
        var moviehall = await _repo.GetMovieHall(moviehallid);
        if (moviehall == null) return NotFound();
        return moviehall;
    }

    // PUT: api/MovieHalls/5
    [HttpPut("{moviehallid}")]
    public async Task<IActionResult> PutMovieHall(int? moviehallid, MovieHall moviehall)
    {
        var success = await _repo.PutMovieHall(moviehallid, moviehall);
        if (!success) return BadRequest();
        return NoContent();
    }

    // POST: api/MovieHalls
    [HttpPost]
    public async Task<ActionResult<MovieHall>> PostMovieHall(MovieHall moviehall)
    {
        var created = await _repo.PostMovieHall(moviehall);
        return CreatedAtAction(nameof(GetMovieHall), new { moviehallid = created.MovieHallId }, created);
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