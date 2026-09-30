using Cinema2026.Repo.Data;
using Cinema2026.Repo.Interfaces;
using Cinema2026.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema2026.Repo.Repositories
{
    // Man skal se et repository som et slags lager. Controlleren er ekspidienten, der taler med kunden, men det er ikke ham der roder rundt på hylderne. Han spørger lagermedarbejderen ("Hent mig person 5"). Og Lagermedarbejderen (Repository) er den eneste der rent faktisk går ind i databasen og henter tingene.
    // Dette er en rigtig god måde at fremtidsikrer et projekt på, da man kan skifte database uden at skulle ændre i controlleren. Man kan også skifte hele repository ud med en anden implementation, uden at controlleren skal ændres.

    public class PersonRepositories : IPersonRepositories // Denne klasse lover at overholde kontrakten i IPersonRepositories. Det betyder, at den skal implementere alle metoderne i interfacet.
    {
        private readonly DatabaseContext context; // Dette er en reference til databasen.

        // Dette er konstruktøren. Den bliver kaldt når man laver et nyt objekt af klassen. Den tager en parameter af typen DatabaseContext, som den gemmer i context feltet. Det betyder, at man kan bruge context i alle metoder i klassen.
        public PersonRepositories(DatabaseContext d)
        {
            context = d;
        }


        // Hente data
        // Denne metode henter alle personer fra databasen. Den returnerer en liste med alle objekter af typen person. 
        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await context.Persons.ToListAsync();
        }

        // Hente data
        // Denne metode henter en person fra databasen ud fra personid. Den returnerer et objekt af typen person.
        public async Task<Person> GetPerson(int personid)
        {
            return await context.Persons.FirstOrDefaultAsync(p => p.PersonId == personid);
        }

        // Opdatere en allerede eksisterende person
        // Denne metode bliver kladt når nogen booker en film
        public async Task<bool> PutPerson(int? personid, Person person)
        {
            // Findes personen?
            if (personid == null || person == null || personid != person.PersonId)
                return false;

            // dette er kernen. Det fortæller Entity Framework: "dette objekt jeg giver dig, skal du betragte som ændret — næste gang du snakker med databasen, skal du opdatere hele rækken til at matche dette objekt."
            context.Entry(person).State = EntityState.Modified;
            await context.SaveChangesAsync(); // Det der rent faktisk der rent faktisk sender en UPDATE til databasen.
            return true;
        }

        // Opretter en ny person
        public async Task<Person> PostPerson(Person person)
        {
            await context.Persons.AddAsync(person); // ligger en ny person ind i hukommelsen (Ikke databsen endnu)
            await context.SaveChangesAsync(); // Det der rent faktisk ligger den nye person ind i databsen
            return person; // Returnere den nye person
        }

        // Sletter en person
        public async Task<bool> DeletePerson(int? personid)
        {
            if (personid == null) return false;

            var p = await context.Persons.FindAsync(personid.Value);
            if (p == null) return false; // personen findes ikke, og vi kan ikke slette npget der ikke findes.

            context.Persons.Remove(p); // Sltter personen i hukommelsen
            await context.SaveChangesAsync(); // Sletter personen fra databasen
            return true; // returnerer true hvis det lykkedes.
        }

        // Bruges i login i Authcontroller. Finder en person på username i stedet for id
        public async Task<Person?> GetByUsername(string username)
        {
            return await context.Persons.FirstOrDefaultAsync(p => p.username == username);
        }

        // Bruges af register. Tjekker om brugernavnet allerede findes inden den prøver at oprette brugeren
        public async Task<bool> UsernameExists(string username)
        {
            return await context.Persons.AnyAsync(p => p.username == username);
        }
    }
}