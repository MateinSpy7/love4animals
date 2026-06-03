namespace love4animals.DTOs;

public record GetCampaignDto(Guid Id, string Title, string Description, decimal GoalAmount);
public record CreateCampaignDto(string Title, string Description, decimal GoalAmount, Guid CreatorId);
public record UpdateCampaignDto(string Title, string Description, decimal GoalAmount);