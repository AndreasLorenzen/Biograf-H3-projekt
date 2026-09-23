using Cinema2026.Repo.Data;
using Cinema2026.Repo.Interfaces;
using Cinema2026.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema2026.Repo.Repositories
{
    public class AdminRepositories : IAdminRepositories
    {
        private readonly DatabaseContext context;

        public AdminRepositories(DatabaseContext d)
        {
            context = d;
        }

        public async Task<IEnumerable<Admin>> GetAdmins()
        {
            return await context.Admins.ToListAsync();
        }

        public async Task<Admin> GetAdmin(int adminid)
        {
            return await context.Admins.FirstOrDefaultAsync(a => a.AdminId == adminid);
        }

        public async Task<bool> PutAdmin(int? adminid, Admin admin)
        {
            if (adminid == null || admin == null || adminid != admin.AdminId)
                return false;

            context.Entry(admin).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Admin> PostAdmin(Admin admin)
        {
            context.Admins.Add(admin);
            await context.SaveChangesAsync();
            return admin;
        }

        public async Task<bool> DeleteAdmin(int? adminid)
        {
            if (adminid == null) return false;
            var a = await context.Admins.FindAsync(adminid.Value);
            if (a == null) return false;
            context.Admins.Remove(a);
            await context.SaveChangesAsync();
            return true;
        }
    }
}