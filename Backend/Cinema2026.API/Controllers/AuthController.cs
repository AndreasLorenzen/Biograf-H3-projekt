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
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
        }

        // GET: api/Auth/me
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<Person>> Me()
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (idClaim == null || !int.TryParse(idClaim, out var personId))
                return Unauthorized();

            var person = await personRepo.GetPerson(personId);
            if (person == null) return Unauthorized();

            return person;
        }

        private async Task SignInPerson(Person person)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, person.PersonId.ToString()),
                new(ClaimTypes.Name, person.username!)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                });
        }
    }

    public sealed class LoginRequest
    {
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}