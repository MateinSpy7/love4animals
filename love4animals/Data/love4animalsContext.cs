using love4animals.Models; // Importamos la capa de Dominio
using Microsoft.EntityFrameworkCore;

namespace love4animals.Data; // O el namespace que estés usando para tu infraestructura

public class Love4AnimalsDbContext : DbContext
{
    // El constructor recibe las opciones desde Program.cs (como la cadena de conexión)
    public Love4AnimalsDbContext(DbContextOptions<Love4AnimalsDbContext> options) : base(options)
    {
    }

    // Aquí le decimos a EF Core: "Convierte estas clases en Tablas en PostgreSQL"
    // El nombre de la propiedad (ej. 'Users') será el nombre exacto de la tabla en la base de datos.
    public DbSet<User> Users { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Donation> Donations { get; set; }

    // (Opcional por ahora) Aquí es donde en el futuro puedes escribir reglas especiales, 
    // como decirle que un campo sea único, o cambiar el nombre de una columna, usando Fluent API.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configuraciones adicionales irían aquí
    }
}