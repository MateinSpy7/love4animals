using love4animals.Models;

namespace love4animals.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly List<Campaign> _data = new();

    public IEnumerable<Campaign> GetAll() => _data;
    public Campaign? GetById(Guid id) => _data.FirstOrDefault(c => c.Id == id);
    public void Add(Campaign campaign) => _data.Add(campaign);
    
    public void Update(Campaign campaign)
    {
        var existing = GetById(campaign.Id);
        if (existing != null)
        {
            existing.Title = campaign.Title;
            existing.Description = campaign.Description;
            existing.GoalAmount = campaign.GoalAmount;
        }
    }

    public void Delete(Guid id) => _data.RemoveAll(c => c.Id == id);
}