using love4animals.DTOs;

namespace love4animals.Services;

public interface IDonationService
{
    IEnumerable<GetDonationDto> GetAll();
    GetDonationDto? GetById(Guid id);
    GetDonationDto Create(CreateDonationDto dto);
    GetDonationDto? Update(Guid id, UpdateDonationDto dto);
    bool Delete(Guid id);
}