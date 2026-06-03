using love4animals.DTOs;
using love4animals.Repositories;
using love4animals.Models;

using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace love4animals.Services;

public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository _repo;
    private readonly IDistributedCache _cache; //Redis

    public CampaignService(ICampaignRepository repo, IDistributedCache cache)
    {
        _repo = repo;
        _cache = cache;
    }

    public IEnumerable<GetCampaignDto> GetAll()
    {
        
        string cacheKey = "campaigns:all";
        var cachedData = _cache.GetString(cacheKey);

        if (cachedData != null)
        {
            // si estaba en redis
            return JsonSerializer.Deserialize<List<GetCampaignDto>>(cachedData)!;
        }

        //no estaba en redis va a la bd
        var campaigns = _repo.GetAll().Select(c => new GetCampaignDto(c.Id, c.Title, c.Description, c.GoalAmount)).ToList();
        
        // Guardamos en Redis por 10 minutos
        _cache.SetString(cacheKey, JsonSerializer.Serialize(campaigns), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });

        return campaigns;
    }

    public GetCampaignDto? GetById(Guid id)
    {
        var campaign = _repo.GetById(id);
        return campaign == null ? null : new GetCampaignDto(campaign.Id, campaign.Title, campaign.Description, campaign.GoalAmount);
    }

    public GetCampaignDto Create(CreateCampaignDto dto)
    {
        var campaign = new Campaign { Title = dto.Title, Description = dto.Description, GoalAmount = dto.GoalAmount, CreatorId = dto.CreatorId };
        _repo.Add(campaign);
        
        _cache.Remove("campaigns:all"); //Borramos caché vieja

        return new GetCampaignDto(campaign.Id, campaign.Title, campaign.Description, campaign.GoalAmount);
    }

    public bool Update(Guid id, UpdateCampaignDto dto)
    {
        var existing = _repo.GetById(id);
        if (existing == null) return false;

        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.GoalAmount = dto.GoalAmount;
        _repo.Update(existing);
        
        _cache.Remove("campaigns:all"); 

        return true;
    }

    public bool Delete(Guid id)
    {
        if (_repo.GetById(id) == null) return false;
        _repo.Delete(id);
        
        _cache.Remove("campaigns:all"); 
        
        return true;
    }
}