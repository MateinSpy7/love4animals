using love4animals.DTOs;

namespace love4animals.Services;

public interface ICampaignService
{
    IEnumerable<GetCampaignDto> GetAll();
    GetCampaignDto? GetById(Guid id);
    GetCampaignDto Create(CreateCampaignDto dto);
    bool Update(Guid id, UpdateCampaignDto dto);
    bool Delete(Guid id);
}