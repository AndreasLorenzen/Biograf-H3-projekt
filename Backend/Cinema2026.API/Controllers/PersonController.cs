using Cinema2026.API.Dtos;
using Cinema2026.Repo.Interfaces;
using Cinema2026.Repo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema2026.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepositories personRepo;

        public PersonController(IPersonRepositories r)
        {
            personRepo = r;
        }

        // Lille hjælpe-metode: oversætter en Person (model) til en PersonReadDto (det vi viser klienten)
        private PersonReadDto ToReadDto(Person p)
        {
            return new PersonReadDto
            {
                PersonId = p.PersonId,
                name = p.name,
                age = p.age,
                Password = p.Password,
                Email = p.Email,
                MovieHallId = p.MovieHallId
            };
        }

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonReadDto>>> GetPersons()
        {
            var persons = await personRepo.GetPersons();

            // .Select() oversætter hver Person i listen til en PersonReadDto
            var result = persons.Select(ToReadDto);

            return Ok(result);
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonReadDto>> GetPerson(int id)
        {
            var person = await personRepo.GetPerson(id);
            if (person == null) return NotFound();

            return ToReadDto(person);
        }

        // POST: api/Person
        [HttpPost]
        public async Task<ActionResult<PersonReadDto>> PostPerson(PersonCreateDto dto)
        {
            // Byg en "rigtig" Person ud fra DTO'en - PersonId sættes ikke, databasen genererer det
            var person = new Person
            {
                name = dto.name,
                age = dto.age,
                Password = dto.Password,
                Email = dto.Email,
                MovieHallId = dto.MovieHallId
            };

            var created = await personRepo.PostPerson(person);

            // Returnér den oprettede person som en ReadDto, med 201 Created
            return CreatedAtAction(nameof(GetPerson), new { id = created.PersonId }, ToReadDto(created));
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, PersonUpdateDto dto)
        {
            // Byg en Person-model ud fra ID (fra URL) + DTO (fra body)
            var person = new Person
            {
                PersonId = id,
                name = dto.name,
                age = dto.age,
                Password = dto.Password,
                Email = dto.Email,
                MovieHallId = dto.MovieHallId
            };

            var success = await personRepo.PutPerson(id, person);
            if (!success) return BadRequest();

            return NoContent();
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var success = await personRepo.DeletePerson(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}