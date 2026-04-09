namespace love4animals.DTOs;

public record GetDonationDto(Guid Id, decimal Amount, DateTime CreatedAt, Guid UserId, Guid? CampaignId, Guid? PostId);
public record CreateDonationDto(decimal Amount, Guid UserId, Guid? CampaignId, Guid? PostId);
public record UpdateDonationDto(decimal Amount);