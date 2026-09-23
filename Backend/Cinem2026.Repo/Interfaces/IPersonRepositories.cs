using Cinema2026.Repo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cinema2026.Repo.Interfaces
{
    public interface IPersonRepositories
    {
        Task<IEnumerable<Person>> GetPersons();
        Task<Person> GetPerson(int personid);
        Task<bool> PutPerson(int? personid, Person person);
        Task<Person> PostPerson(Person person);
        Task<bool> DeletePerson(int? personid);

    }
}
