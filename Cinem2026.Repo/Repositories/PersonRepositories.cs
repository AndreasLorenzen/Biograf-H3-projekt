using Cinema2026.Repo.Interfaces;
using Cinema2026.Repo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema2026.Repo.Repositories
{
    public class PersonRepositories:IPersonRepositories
    {
        // create 
        // get
        List<Person> persons = new List<Person>()
        {
            new Person() { Id = 1, name = "John", age = 30 },
            new Person() { Id = 2, name = "Jane", age = 25 },
            new Person() { Id = 3, name = "Bob", age = 40 }
        };
        // using my persons list
        public List<Person> GetPersons()
        {
            return persons;
        }
    }
}
