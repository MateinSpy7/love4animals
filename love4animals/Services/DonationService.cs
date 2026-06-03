using love4animals.DTOs;
using love4animals.Models;
using love4animals.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace love4animals.Services;

public class DonationService : IDonationService
{
    private readonly IDonationRepository _repo;
    private readonly ICampaignRepository _campaignRepo;
    private readonly IDistributedCache _cache;

    public DonationService(IDonationRepository repo, ICampaignRepository campaignRepo, IDistributedCache cache)
    {
        _repo = repo;
        _campaignRepo = campaignRepo;
        _cache = cache;
    }

    public IEnumerable<GetDonationDto> GetAll()
    {
        // CACHE-ASIDE
        string cacheKey = "donations:all";
        var cachedData = _cache.GetString(cacheKey);

        if (cachedData != null)
        {
            return JsonSerializer.Deserialize<List<GetDonationDto>>(cachedData)!;
        }

        var donations = _repo.GetAll().Select(d => new GetDonationDto(d.Id, d.Amount, d.CreatedAt, d.UserId, d.CampaignId, d.PostId)).ToList();
        
        _cache.SetString(cacheKey, JsonSerializer.Serialize(donations), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });

        return donations;
    }

    public GetDonationDto? GetById(Guid id)
    {
        var d = _repo.GetById(id);
        return d == null ? null : new GetDonationDto(d.Id, d.Amount, d.CreatedAt, d.UserId, d.CampaignId, d.PostId);
    }

    public GetDonationDto Create(CreateDonationDto dto)
    {
        var donation = new Donation
        {
            Amount = dto.Amount,
            UserId = dto.UserId,
            CampaignId = dto.CampaignId,
            PostId = dto.PostId
        };
        _repo.Add(donation);

        if (dto.CampaignId.HasValue)
        {
            var campaign = _campaignRepo.GetById(dto.CampaignId.Value);
            if (campaign != null)
            {
                campaign.CurrentAmount += dto.Amount;
                _campaignRepo.Update(campaign);
                _cache.Remove("campaigns:all"); 
            }
        }

        _cache.Remove("donations:all"); 

        return new GetDonationDto(donation.Id, donation.Amount, donation.CreatedAt, donation.UserId, donation.CampaignId, donation.PostId);
    }

    public GetDonationDto? Update(Guid id, UpdateDonationDto dto)
    {
        var existing = _repo.GetById(id);
        if (existing == null) return null;

        existing.Amount = dto.Amount;
        _repo.Update(existing);
        
        _cache.Remove("donations:all"); //invalido donaciones

        return new GetDonationDto(existing.Id, existing.Amount, existing.CreatedAt, existing.UserId, existing.CampaignId, existing.PostId);
    }

    public bool Delete(Guid id)
    {
        if (_repo.GetById(id) == null) return false;
        _repo.Delete(id);
        
        _cache.Remove("donations:all"); 

        return true;
    }
}