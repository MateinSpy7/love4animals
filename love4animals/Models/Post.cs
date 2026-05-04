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

    public ICollection<Comment> Comments { get; set; } = new List<Comment>(); // Un post puede tener muchos comentarios
    public User Missionary { get; set; } = null!; // Un post tiene un misionero, es decir un usuario que lo publicó
    public Campaign? Campaign { get; set; } // Un post puede pertenecer a una campaña, pero no es obligatorio
     
}