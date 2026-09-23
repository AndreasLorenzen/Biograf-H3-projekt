using Cinema2026.Repo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema2026.Repo.Interfaces
{
    public interface IAdminRepositories
    {
        public Task<IEnumerable<Admin>> GetAdmins();
        public Task<Admin> GetAdmin(int adminid);
        public Task<bool> PutAdmin(int? adminid, Admin admin);
        public Task<Admin> PostAdmin(Admin admin);
        public Task<bool> DeleteAdmin(int? adminid);
    }
}