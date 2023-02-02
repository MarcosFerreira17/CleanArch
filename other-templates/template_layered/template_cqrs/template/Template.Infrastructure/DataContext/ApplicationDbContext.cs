using Template.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Template.Infrastructure.DataContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<Entity> Entity { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}