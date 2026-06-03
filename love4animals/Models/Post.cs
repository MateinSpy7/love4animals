namespace love4animals.Models;

public class Post
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Content { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
   
    public Guid MissionaryId { get; set; } 
    public Guid? CampaignId { get; set; } 

    public ICollection<Comment> Comments { get; set; } = new List<Comment>(); 
    public User Missionary { get; set; } = null!; 
    public Campaign? Campaign { get; set; } 
}