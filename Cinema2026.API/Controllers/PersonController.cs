using Cinema2026.API.Models;
using Cinema2026.Repo.Data;
using Cinema2026.Repo.Interfaces;
using Cinema2026.Repo.Models;
using Cinema2026.Repo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cinema2026.API.Controllers
{
    [Route("api/[controller]")] // https://localhost:7073/api/person
    [ApiController]
    public class PersonController : ControllerBase
    {
        // this class uses Repository. to do so we instance an object
        // variable of type PersonRepositories

        IPersonRepositories personRepo; // = new PersonRepositories();
        private readonly DatabaseContext context;

        public PersonController(IPersonRepositories r, DatabaseContext d)
        {
            personRepo = r;
            context = d;
        }

        // GET api/Person
        [HttpGet]
        public List<Person> GetPersons()
        {
            return personRepo.GetPersons();
        }

        // GET api/Person/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPersonAsync(int id)
        {
            var person = await context.Persons.FirstOrDefaultAsync(p => p.PersonId == id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // POST api/Person
        [HttpPost]
        public async Task<Person> CreatePerson(Person person)
        {
            context.Persons.Add(person);
            await context.SaveChangesAsync();
            return person;
        }

        // DELETE api/Person/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonAsync(int id)
        {
            var person = await context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            context.Persons.Remove(person);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}