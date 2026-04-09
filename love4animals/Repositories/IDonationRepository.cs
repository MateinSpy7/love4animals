using love4animals.Models;

namespace love4animals.Repositories;

public interface IDonationRepository
{
    IEnumerable<Donation> GetAll();
    Donation? GetById(Guid id);
    void Add(Donation donation);
    void Update(Donation donation);
    void Delete(Guid id);
}