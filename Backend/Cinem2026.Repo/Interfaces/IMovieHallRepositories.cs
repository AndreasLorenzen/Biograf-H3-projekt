using Cinema2026.Repo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cinema2026.Repo.Interfaces
{
    public interface IMovieHallRepositories
    {

        public Task<IEnumerable<MovieHall>> GetMovieHall();

        public Task<MovieHall> GetMovieHall(int moviehallid);

        public Task<bool> PutMovieHall(int? moviehallid, MovieHall moviehall);

        public Task<MovieHall> PostMovieHall(MovieHall moviehall);

        public Task<bool> DeleteMovieHall(int? moviehallid);

    }
}
