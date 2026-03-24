using love4animals.Models;

namespace love4animals.Repositories;

public interface ICampaignRepository
{
    IEnumerable<Campaign> GetAll();
    Campaign? GetById(Guid id);
    void Add(Campaign campaign);
    void Update(Campaign campaign);
    void Delete(Guid id);
}