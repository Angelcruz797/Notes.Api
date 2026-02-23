using Microsoft.EntityFrameworkCore;
using Notes.Api.Models;

namespace Notes.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Files> Files => Set<Files>();
}