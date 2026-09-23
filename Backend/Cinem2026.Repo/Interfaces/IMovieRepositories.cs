using Cinema2026.Repo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema2026.Repo.Interfaces
{
    public interface IMovieRepositories
    {
        public Task<IEnumerable<Movie>> GetMovies();
        public Task<Movie> GetMovie(int movieid);
        public Task<bool> PutMovie(int? movieid, Movie movie);
        public Task<Movie> PostMovie(Movie movie);
        public Task<bool> DeleteMovie(int? movieid);
    }
}