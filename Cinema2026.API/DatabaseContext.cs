using Microsoft.EntityFrameworkCore;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Cinema2026.API.Models.Person1> Person1 { get; set; } = default!;
}
