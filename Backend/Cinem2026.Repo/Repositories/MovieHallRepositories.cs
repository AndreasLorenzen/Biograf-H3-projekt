using Cinema2026.Repo.Data;
using Cinema2026.Repo.Interfaces;
using Cinema2026.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cinema2026.Repo.Repositories
{
    public class MovieHallRepositories : IMovieHallRepositories
    {
        private readonly DatabaseContext context;
        public MovieHallRepositories(DatabaseContext d)
        {
            context = d;
        }

        public async Task<IEnumerable<MovieHall>> GetMovieHall()
        {
            // Henter alle sale, og deres tilknyttede personer med det samme
            return await context.MovieHalls.ToListAsync();
        }

        public async Task<MovieHall> GetMovieHall(int moviehallid)
        {
            // Henter én sal ud fra ID, inklusiv dens tilknyttede personer
            return await context.MovieHalls
                .FirstOrDefaultAsync(m => m.MovieHallId == moviehallid);
        }

        public async Task<bool> PutMovieHall(int? moviehallid, MovieHall moviehall)
        {
            if (moviehallid == null || moviehall == null || moviehallid != moviehall.MovieHallId)
                return false;

            context.Entry(moviehall).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<MovieHall> PostMovieHall(MovieHall moviehall)
        {
            context.MovieHalls.Add(moviehall);
            await context.SaveChangesAsync();
            return moviehall;
        }

        public async Task<bool> DeleteMovieHall(int? moviehallid)
        {
            if (moviehallid == null) return false;
            var mh = await context.MovieHalls.FindAsync(moviehallid.Value);
            if (mh == null) return false;
            context.MovieHalls.Remove(mh);
            await context.SaveChangesAsync();
            return true;
        }
    }
}