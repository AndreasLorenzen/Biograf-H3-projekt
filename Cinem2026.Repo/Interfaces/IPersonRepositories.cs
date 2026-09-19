using Cinema2026.Repo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cinema2026.Repo.Interfaces
{
    public interface IPersonRepositories
    {
        public List<Person> GetPersons();

        public Task<Person> GetPersonAsync(int id);

        //public Task<Person> DeletePersonAsync(int id);

        public Task<Person> CreatePerson(Person person);

    }
}
