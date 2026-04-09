using love4animals.DTOs;
using love4animals.Models;
using love4animals.Repositories;

namespace love4animals.Services;

public class DonationService : IDonationService
{
    private readonly IDonationRepository _repo;
    private readonly ICampaignRepository _campaignRepo; //para actualizar los fondos

    // Inyectamos ambos repositorios, el de donaciones y el de campañas
    public DonationService(IDonationRepository repo, ICampaignRepository campaignRepo)
    {
        _repo = repo;
        _campaignRepo = campaignRepo;
    }

    public IEnumerable<GetDonationDto> GetAll() =>
        _repo.GetAll().Select(d => new GetDonationDto(d.Id, d.Amount, d.CreatedAt, d.UserId, d.CampaignId, d.PostId));

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

        // LÓGICA DE NEGOCIO: Si donaron a una campaña, actualizamos el total recaudado
        if (dto.CampaignId.HasValue)
        {
            var campaign = _campaignRepo.GetById(dto.CampaignId.Value);
            if (campaign != null)
            {
                campaign.CurrentAmount += dto.Amount;
                _campaignRepo.Update(campaign);
            }
        }

        return new GetDonationDto(donation.Id, donation.Amount, donation.CreatedAt, donation.UserId, donation.CampaignId, donation.PostId);
    }

    public GetDonationDto? Update(Guid id, UpdateDonationDto dto)
    {
        var existing = _repo.GetById(id);
        if (existing == null) return null;

        existing.Amount = dto.Amount;
        _repo.Update(existing);
        
        return new GetDonationDto(existing.Id, existing.Amount, existing.CreatedAt, existing.UserId, existing.CampaignId, existing.PostId);
    }

    public bool Delete(Guid id)
    {
        if (_repo.GetById(id) == null) return false;
        _repo.Delete(id);
        return true;
    }
}