using Cinema2026.Repo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cinema2026.Repo.Interfaces
{
    // Et interface er en kontrakt man kan sætte på en klasse, så den klasse skal opfylde de krav der er i interfacet.
    // et interface indeholder sjællent noget rigtig kode, men bare de funktioner der skal være i den pågældene klasse.
    public interface IPersonRepositories
    {
        Task<IEnumerable<Person>> GetPersons();
        Task<Person> GetPerson(int personid);
        Task<bool> PutPerson(int? personid, Person person);
        Task<Person> PostPerson(Person person);
        Task<bool> DeletePerson(int? personid);

        Task<Person?> GetByUsername(string username);
        Task<bool> UsernameExists(string username);
    }
}
