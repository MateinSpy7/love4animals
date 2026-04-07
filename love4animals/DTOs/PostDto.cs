namespace love4animals.DTOs;

public record GetPostDto(Guid Id, string Content, string ImageUrl, DateTime CreatedAt, Guid MissionaryId, Guid? CampaignId);

public record CreatePostDto(string Content, string ImageUrl, Guid MissionaryId, Guid? CampaignId);

public record UpdatePostDto(string Content, string ImageUrl, Guid? CampaignId);