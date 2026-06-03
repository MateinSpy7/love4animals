namespace love4animals.Models;

public class Donation
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    
    public Guid UserId { get; set; }
    public Guid? CampaignId { get; set; } 
    public Guid? PostId { get; set; }    

    public User Donor { get; set; } = null!; 
    public Campaign? Campaign { get; set; }
    public Post? Post { get; set; } 
}