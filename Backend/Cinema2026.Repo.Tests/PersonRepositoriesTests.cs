using Cinema2026.Repo.Data;
using Cinema2026.Repo.Models;
using Cinema2026.Repo.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Cinema2026.Repo.Tests
{
    public class PersonRepositoriesTests
    {
        // Dette er en hjælpemetode. Den laver en hurtig, tom test-database i hukommelsen
        // Filen indeholder også noget navigation fra en side til en anden
        private DatabaseContext OpretTestDatabase(string dbNavn)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(dbNavn)
                .Options;

            return new DatabaseContext(options);
        }

        [Fact]
        // Opretter en testdatabase, og sætter en person ind. Derefter hentes personen
        public async Task GetByUsername_Finder_Person_Når_Brugere_Eksisterer()
        {
            var context = OpretTestDatabase("Db_GetByUsername");
            context.Persons.Add(new Person { username = "alice", password = "pw", email = "alice@test.dk" });
            await context.SaveChangesAsync();

            var repository = new PersonRepositories(context);

            var funnetPerson = await repository.GetByUsername("alice");

            Assert.NotNull(funnetPerson);
            Assert.Equal("alice", funnetPerson.username);
        }

        [Fact]
        // Opretter en testdatabase, og laver en person. Derefter tjekkes der om personen findes
        public async Task UsernameExists_Returnerer_True_Når_Bruger_Findes()
        {
            var context = OpretTestDatabase("Db_UsernameExists");
            context.Persons.Add(new Person { username = "bob", password = "pw", email = "bob@test.dk" });
            await context.SaveChangesAsync();

            var repository = new PersonRepositories(context);

            var findes = await repository.UsernameExists("bob");

            Assert.True(findes);
        }

        [Fact]
        // Opretter en testdatabase og opretter en ny person, og tjekker om den er oprettet i testdatabasen
        public async Task PostPerson_Gemmer_Person_I_Databasen()
        {
            var context = OpretTestDatabase("Db_PostPerson");
            var repository = new PersonRepositories(context);
            var nyPerson = new Person { username = "carol", password = "pw", email = "carol@test.dk", age = 25 };

            var gemtPerson = await repository.PostPerson(nyPerson);

            Assert.NotEqual(0, gemtPerson.PersonId);
        }

        [Fact]
        // Opretter en testdatabase, og sætter en person ind. Derefter slettes denne person
        public async Task DeletePerson_Fjerner_Person_Fra_Databasen()
        {
            var context = OpretTestDatabase("Db_DeletePerson");
            var repository = new PersonRepositories(context);

            var person = new Person { username = "carol", password = "pw", email = "carol@test.dk" };
            var gemtPerson = await repository.PostPerson(person);

            var blevSlettet = await repository.DeletePerson(gemtPerson.PersonId);

            Assert.True(blevSlettet);

            var findesStadig = await repository.UsernameExists("carol");
            Assert.False(findesStadig);
        }
    }
}