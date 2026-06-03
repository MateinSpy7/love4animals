using love4animals.Models; 
using Microsoft.EntityFrameworkCore;

namespace love4animals.Data; 
public class Love4AnimalsDbContext : DbContext
{
    public Love4AnimalsDbContext(DbContextOptions<Love4AnimalsDbContext> options) : base(options)
    {
    }

  public DbSet<User> Users { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Donation> Donations { get; set; }
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    
    // Hace que la columna Email sea única en la base de datos
    modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();
}
}