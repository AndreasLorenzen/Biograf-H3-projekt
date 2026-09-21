using Cinema2026.Repo.Data;
using Cinema2026.Repo.Interfaces;
using Cinema2026.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema2026.Repo.Repositories
{
    public class MovieRepositories : IMovieRepositories
    {
        private readonly DatabaseContext context;

        public MovieRepositories(DatabaseContext d)
        {
            context = d;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            // Henter alle film, og de sale de vises i, med det samme
            return await context.Movies.Include(m => m.MovieHalls).ToListAsync();
        }

        public async Task<Movie> GetMovie(int movieid)
        {
            return await context.Movies
                .Include(m => m.MovieHalls)
                .FirstOrDefaultAsync(m => m.MovieId == movieid);
        }

        public async Task<bool> PutMovie(int? movieid, Movie movie)
        {
            if (movieid == null || movie == null || movieid != movie.MovieId)
                return false;

            context.Entry(movie).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Movie> PostMovie(Movie movie)
        {
            context.Movies.Add(movie);
            await context.SaveChangesAsync();
            return movie;
        }

        public async Task<bool> DeleteMovie(int? movieid)
        {
            if (movieid == null) return false;
            var m = await context.Movies.FindAsync(movieid.Value);
            if (m == null) return false;
            context.Movies.Remove(m);
            await context.SaveChangesAsync();
            return true;
        }
    }
}