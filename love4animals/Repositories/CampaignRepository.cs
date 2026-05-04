using love4animals.Models;
using Microsoft.EntityFrameworkCore;
using love4animals.Data;
namespace love4animals.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly Love4AnimalsDbContext _context;

    public CampaignRepository(Love4AnimalsDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Campaign> GetAll()
    {
        return _context.Campaigns.ToList();
    }
    public Campaign? GetById(Guid id) 
    {
        return _context.Campaigns.FirstOrDefault(c => c.Id == id);
    }
    public void Add(Campaign campaign)
    {
        _context.Campaigns.Add(campaign);
        _context.SaveChanges();
    }
    
    public void Update(Campaign campaign)
    {
        var existing = GetById(campaign.Id);
        if (existing != null)
        {
            _context.Campaigns.Update(campaign);
            _context.SaveChanges();
            // Solo puede cambiar la descripción y el monto objetivo de la campaña
            // existing.Description = campaign.Description; 
            // existing.GoalAmount = campaign.GoalAmount;
        }
    }

    public void Delete(Guid id)
    {
        var campaign = GetById(id);
        if (campaign != null)
        {
            _context.Campaigns.Remove(campaign);
            _context.SaveChanges();
        }
    }
}