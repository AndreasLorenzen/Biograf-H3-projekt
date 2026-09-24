using Microsoft.AspNetCore.Mvc;
using Cinema2026.Repo.Models;
using Cinema2026.Repo.Interfaces;
using Cinema2026.API.Dtos;

namespace Cinema2026.API.Controllers
{
    [Route("api/[controller]")] // https://localhost:7073/api/movie
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepositories _repo;

        public MovieController(IMovieRepositories repo)
        {
            _repo = repo;
        }

        //private MovieReadDto ToReadDto(Movie m)
        //{
        //    return new MovieReadDto
        //    {
        //        MovieId = m.MovieId,
        //        Moviename = m.Moviename,
        //        Movieage = m.Movieage,
        //        //MovieHallIds = m.MovieHalls?.Select(h => h.MovieHallId).ToList() ?? new List<int>()
        //    };
        //}

        // GET: api/Movie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _repo.GetMovies();
            return Ok(movies);
        }

        // GET: api/Movie/5
        [HttpGet("{movieid}")]
        public async Task<ActionResult<Movie>> GetMovie(int movieid)
        {
            var movie = await _repo.GetMovie(movieid);
            if (movie == null) return NotFound();
            return movie;
        }

        // PUT: api/Movie/5
        [HttpPut("{movieid}")]
        public async Task<IActionResult> PutMovie(int movieid, Movie movie)
        {
            if (movieid != movie.MovieId)
            {
                return BadRequest();
            }

            var success = await _repo.PutMovie(movieid, movie);
            if (!success) return BadRequest();
            return NoContent();
        }

        // POST: api/Movie
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            var created = await _repo.PostMovie(movie);
            return CreatedAtAction(nameof(GetMovie), new { movieid = created.MovieId }, created);
        }

        // DELETE: api/Movie/5
        [HttpDelete("{movieid}")]
        public async Task<IActionResult> DeleteMovie(int movieid)
        {
            var success = await _repo.DeleteMovie(movieid);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}