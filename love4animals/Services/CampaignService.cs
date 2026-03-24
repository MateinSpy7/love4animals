using love4animals.DTOs;
using love4animals.Repositories;
using love4animals.Models;

namespace love4animals.Services;

public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository _repo;

    public CampaignService(ICampaignRepository repo) => _repo = repo;

    public IEnumerable<GetCampaignDto> GetAll() => 
        _repo.GetAll().Select(c => new GetCampaignDto(c.Id, c.Title, c.Description, c.GoalAmount));

    public GetCampaignDto? GetById(Guid id)
    {
        var campaign = _repo.GetById(id);
        return campaign == null ? null : new GetCampaignDto(campaign.Id, campaign.Title, campaign.Description, campaign.GoalAmount);
    }

    public GetCampaignDto Create(CreateCampaignDto dto)
    {
        var campaign = new Campaign { Title = dto.Title, Description = dto.Description, GoalAmount = dto.GoalAmount };
        _repo.Add(campaign);
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
        return true;
    }

    public bool Delete(Guid id)
    {
        if (_repo.GetById(id) == null) return false;
        _repo.Delete(id);
        return true;
    }
}