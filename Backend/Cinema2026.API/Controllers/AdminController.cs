using Microsoft.AspNetCore.Mvc;
using Cinema2026.Repo.Models;
using Cinema2026.Repo.Interfaces;
using Cinema2026.API.Dtos;

namespace Cinema2026.API.Controllers
{
    [Route("api/[controller]")] // https://localhost:7073/api/admin
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepositories _repo;

        public AdminController(IAdminRepositories repo)
        {
            _repo = repo;
        }

        private AdminReadDto ToReadDto(Admin a)
        {
            return new AdminReadDto
            {
                AdminId = a.AdminId,
                Username = a.Username,
                Email = a.Email
                // Password bliver bevidst IKKE mappet med her
            };
        }

        // GET: api/Admin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            var admins = await _repo.GetAdmins();
            return Ok(admins);
        }

        // GET: api/Admin/5
        [HttpGet("{adminid}")]
        public async Task<ActionResult<Admin>> GetAdmin(int adminid)
        {
            var admin = await _repo.GetAdmin(adminid);
            if (admin == null) return NotFound();
            return admin;
        }

        // PUT: api/Admin/5
        [HttpPut("{adminid}")]
        public async Task<IActionResult> PutAdmin(int adminid, Admin admin)
        {
            // Sikrer at ID'et i URL'en matcher ID'et i objektet, man sender
            if (adminid != admin.AdminId)
            {
                return BadRequest();
            }

            var success = await _repo.PutAdmin(adminid, admin);
            if (!success) return BadRequest();
            return NoContent();
        }

        // POST: api/Admin
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            var created = await _repo.PostAdmin(admin);
            return CreatedAtAction(nameof(GetAdmin), new { adminid = created.AdminId }, created);
        }

        // DELETE: api/Admin/5
        [HttpDelete("{adminid}")]
        public async Task<IActionResult> DeleteAdmin(int adminid)
        {
            var success = await _repo.DeleteAdmin(adminid);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}