using Cinema2026.Repo.Data;
using Cinema2026.Repo.Interfaces;
using Cinema2026.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema2026.Repo.Repositories
{
    public class PersonRepositories : IPersonRepositories
    {
        private readonly DatabaseContext context;

        public PersonRepositories(DatabaseContext d)
        {
            context = d;
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await context.Persons.ToListAsync();
        }

        public async Task<Person> GetPerson(int personid)
        {
            return await context.Persons.FirstOrDefaultAsync(p => p.PersonId == personid);
        }

        public async Task<bool> PutPerson(int? personid, Person person)
        {
            if (personid == null || person == null || personid != person.PersonId)
                return false;

            context.Entry(person).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Person> PostPerson(Person person)
        {
            context.Persons.Add(person);
            await context.SaveChangesAsync();
            return person;
        }

        public async Task<bool> DeletePerson(int? personid)
        {
            if (personid == null) return false;

            var p = await context.Persons.FindAsync(personid.Value);
            if (p == null) return false;

            context.Persons.Remove(p);
            await context.SaveChangesAsync();
            return true;
        }
    }
}