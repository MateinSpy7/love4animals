namespace love4animals.DTOs;

public record GetCampaignDto(Guid Id, string Title, string Description, decimal GoalAmount);
public record CreateCampaignDto(string Title, string Description, decimal GoalAmount);
public record UpdateCampaignDto(string Title, string Description, decimal GoalAmount);