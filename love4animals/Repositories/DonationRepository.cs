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
       
        return _context.Donations
            .Include(d => d.Donor)
            .Include(d => d.Campaign)
            .Include(d => d.Post)
            .ToList();
    }

    public Donation? GetById(Guid id) 
    {
        return _context.Donations
            .Include(d => d.Donor) // Agregué el include por si necesitas ver el donante
            .FirstOrDefault(d => d.Id == id);
    }

    public void Add(Donation donation) 
    {
       
        _context.Donations.Add(donation);
        _context.SaveChanges(); 
    }
    
    public void Update(Donation donation)
    {
       
        _context.Donations.Update(donation);
        _context.SaveChanges();
    }
    
    public void Delete(Guid id) 
    {
      
        var existing = GetById(id);
        if (existing != null) 
        {
            _context.Donations.Remove(existing);
            _context.SaveChanges();
        }
    }
}