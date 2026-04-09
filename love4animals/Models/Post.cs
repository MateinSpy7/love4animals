namespace love4animals.Models;

public class Post
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Content { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    //publica un misionero, por eso se llama missionaryId
    //un post puede pertenecer a una campana ampaignId

    public Guid MissionaryId { get; set; } 
    public Guid? CampaignId { get; set; } // El "?" es que no todos los post tienen que pertenecer a una cpaña necesariamente
}