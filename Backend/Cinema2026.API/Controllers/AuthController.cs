using Cinema2026.Repo.Interfaces;
using Cinema2026.Repo.Models;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cinema2026.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPersonRepositories personRepo;

        public AuthController(IPersonRepositories r)
        {
            personRepo = r;
        }

        // POST: api/Auth/register
        // Body: { "username": "...", "password": "...", "personname": "...", "personage": 0 }
        [HttpPost("register")]
        public async Task<ActionResult<Person>> Register(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.username))
                return BadRequest(new { message = "Username is required." });

            if (string.IsNullOrWhiteSpace(person.password))
                return BadRequest(new { message = "Password is required." });

            if (await personRepo.UsernameExists(person.username))
                return Conflict(new { message = "Username is already taken." });

            var created = await personRepo.PostPerson(person);

            // Log the new account in immediately, same as a fresh login
            await SignInPerson(created);

            return CreatedAtAction(nameof(Me), null, created);
        }

        // POST: api/Auth/login
        // Body: { "username": "...", "password": "..." }
        [HttpPost("login")]
        public async Task<ActionResult<Person>> Login(LoginRequest login)
        {
            var person = await personRepo.GetByUsername(login.username);

            if (person == null || person.password != login.password)
                return Unauthorized(new { message = "Invalid username or password." });

            await SignInPerson(person);

            return Ok(person);
        }

        // POST: api/Auth/logout
        // Logout kræver, at brugeren allerede er logget ind.
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
        }

        // GET: api/Auth/me
        // henter nuværende bruger der er logget ind, kræver at brugeren er logget ind.
        // idClaim er en string selvom personId i virkeligheden er en int. cookies bruger strings og derfor er det nødvendigt at pars til int.
        // hvis idClaim er null eller ikke kan parses til int, returner Unauthorized.
        // kort sagt, tjekker Me om der er en bruger logget ind, og slår perssonid op i databasen.
        [HttpGet("me")]
        [Authorize]
        // En vigtig ting med den her metode er, at hvis nu jeg frontend helt ned og åbner den igen, så henter den personen fra databasen igen, i stedet for at gemme den i en cookie. Det er fordi, at hvis jeg ændrer noget i databasen, så ville det ikke blive opdateret i cookien. Det kan være at personen er sat på en anden film eller lignende.
        public async Task<ActionResult<Person>> Me()
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (idClaim == null || !int.TryParse(idClaim, out var personId))
                return Unauthorized();

            var person = await personRepo.GetPerson(personId);
            if (person == null) return Unauthorized();

            return person;
        }

        // Denne metode er som den eneste i Authcontroller private, og den bruges til at logge en person ind ved at oprette en cookie med personens oplysninger.
        // Det er derfor den ikke har nogen HTTPPOST eller GET attribut, da den ikke skal tilgås direkte via HTTP.
        // List<Claim> en lille liste af claim-objekter. Tænk på det, som en indkøbsliste, men hvor element er en påstand i stedet for en vare.
        // I dette tilfælde er påstandene, at brugeren har et bestemt personId og et bestemt brugernavn.
        private async Task SignInPerson(Person person)
        {
            var claims = new List<Claim>
            {
                // Et claim består altid af to ting - en type og en værdi. I dette tilfælde er typen ClaimTypes.NameIdentifier og værdien er person.PersonId.ToString().
                new(ClaimTypes.NameIdentifier, person.PersonId.ToString()),
                new(ClaimTypes.Name, person.username!)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            // dette er den faktiske handling. Alt det ovenfor var bare at forberede dataene. Denne linje tager principal (hele pakken med claims) og bygger en cookie ud af den, som sendes tilbage til browseren i HTTP-svaret.
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    // Den cookie der sendes tilbage til browseren, vil udløbe 7 dage efter den er oprettet.
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                });
        }
    }

    // Dette er en lille klasse der bruges til at modtage login data fra klienten.
    public sealed class LoginRequest
    {
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}