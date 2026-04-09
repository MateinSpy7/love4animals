using love4animals.Models;

namespace love4animals.Repositories;

public class DonationRepository : IDonationRepository
{
    private readonly List<Donation> _data = new();

    public IEnumerable<Donation> GetAll() => _data;
    public Donation? GetById(Guid id) => _data.FirstOrDefault(d => d.Id == id);
    public void Add(Donation donation) => _data.Add(donation);
    
    public void Update(Donation donation)
    {
        var existing = GetById(donation.Id);
        if (existing != null) existing.Amount = donation.Amount;
    }
    
    public void Delete(Guid id) => _data.RemoveAll(d => d.Id == id);
}