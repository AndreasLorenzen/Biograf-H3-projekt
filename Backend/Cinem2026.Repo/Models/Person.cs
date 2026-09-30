using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema2026.Repo.Models
{
    public class Person
    {
        public int PersonId { get; set; } // variable / property -----> primary key
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int age { get; set; }
        public int? MovieId { get; set; }
        public Movie? Movie { get; set; }


    }

    public class MovieHall
    {
        public int MovieHallId { get; set; } // variable / property -----> primary key
        public bool MovieHalloccupied { get; set; }
        public int? MovieId { get; set; } // Foreign key fra Movie 
    }

    public class Movie
    {
        public int MovieId { get; set; } // primary key
        public string movieName { get; set; }
        public int movieDuration { get; set; }
        //public List<MovieHall> MovieHalls { get; set; } = new(); // Navigation property til MovieHall }

    }

    public class Admin
    {
        public int AdminId { get; set; } // primary key
        public string username { get; set; }
        public string password { get; set; } // NB: klartekst for nu - skal hashes når vi laver Auth
        public int adminlevel { get; set; }
    }
}
