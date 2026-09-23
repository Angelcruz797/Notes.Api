using Microsoft.EntityFrameworkCore;
using Notes.Api.Models;

namespace Notes.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Files> Files => Set<Files>();
    public DbSet<Users> Users => Set<Users>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().Ignore(u => u.Files);
    }
}