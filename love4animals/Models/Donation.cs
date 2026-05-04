namespace love4animals.Models;

public class Donation
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    
    public Guid UserId { get; set; }
    public Guid? CampaignId { get; set; } 
    public Guid? PostId { get; set; }    

    public User Donor { get; set; } = null!; // Una donación tiene un donante, es decir un usuario que la realizó
    public Campaign? Campaign { get; set; } // Una donación puede pertenecer a una campaña, pero no es obligatorio
    public Post? Post { get; set; } // Una donación puede estar asociada a un post, pero no es obligatorio

}