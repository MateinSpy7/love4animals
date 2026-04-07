namespace love4animals.Models;

public class Post
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Content { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    // Relaciones
    //el que publica el post es el misionero, por eso se llama missionaryId
    //a que campaña pertenece el post, por eso se llama campaignId

    public Guid MissionaryId { get; set; } 
    public Guid? CampaignId { get; set; } // El "?" es que no todos los post tienen que pertenecer a una cpaña necesariamente
}