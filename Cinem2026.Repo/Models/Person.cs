using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema2026.Repo.Models
{
    public class Person
    {
        public int PersonId { get; set; } // variable / property -----> primary key
        public string Personname { get; set; }
        public int Personage { get; set; }

        public int? MovieHallId { get; set; } // Foreign key fra MovieHall}
        public MovieHall? MovieHall { get; set; } // Navigation property til MovieHall}
    }

    public class MovieHall
    {
        public int MovieHallId { get; set; } // variable / property -----> primary key
        public bool MovieHalloccupied { get; set; }
        public int? MovieId { get; set; } // Foreign key fra Movie 
        public Movie? Movie { get; set; } // Navigation property til Movie}
        public List<Person> Persons { get; set; } = new(); // Navigation property til Person }
    }

    public class Movie
    {
        public int MovieId { get; set; } // primary key
        public string Moviename { get; set; }
        public int Movieage { get; set; }
        public List<MovieHall> MovieHalls { get; set; } = new(); // Navigation property til MovieHall }

    }
}
