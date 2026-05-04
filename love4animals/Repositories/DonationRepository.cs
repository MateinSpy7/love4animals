using love4animals.Models;
using love4animals.Data;
using Microsoft.EntityFrameworkCore;

namespace love4animals.Repositories;

public class DonationRepository : IDonationRepository
{
    private readonly Love4AnimalsDbContext _context;

    // 1. EL CONSTRUCTOR: Es obligatorio para que la API te inyecte la conexión
    public DonationRepository(Love4AnimalsDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Donation> GetAll() 
    {
        // Esto que hiciste está excelente. Los Includes traen la data relacionada.
        return _context.Donations
            .Include(d => d.Donor)
            .Include(d => d.Campaign)
            .Include(d => d.Post)
            .ToList();
    }

    public Donation? GetById(Guid id) 
    {
        // 2. CAMBIO: Reemplazamos '_data' por '_context.Donations'
        return _context.Donations
            .Include(d => d.Donor) // Agregué el include por si necesitas ver el donante
            .FirstOrDefault(d => d.Id == id);
    }

    public void Add(Donation donation) 
    {
        // 3. CAMBIO: Agregar y ejecutar SaveChanges()
        _context.Donations.Add(donation);
        _context.SaveChanges(); 
    }
    
    public void Update(Donation donation)
    {
        // Con EF Core, el método Update se encarga de todo el rastreo
        _context.Donations.Update(donation);
        _context.SaveChanges();
    }
    
    public void Delete(Guid id) 
    {
        // Buscamos la donación en la BD real y la eliminamos
        var existing = GetById(id);
        if (existing != null) 
        {
            _context.Donations.Remove(existing);
            _context.SaveChanges();
        }
    }
}