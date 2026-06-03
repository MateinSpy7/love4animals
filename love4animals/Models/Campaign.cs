namespace love4animals.Models;

public class Campaign
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal GoalAmount { get; set; }
    public decimal CurrentAmount { get; set; } = 0;

    public Guid CreatorId { get; set; } 
public User Creator { get; set; } = null!; 
    public ICollection<Post> Posts { get; set; } = new List<Post>(); 
    public ICollection<Donation> Donations { get; set; } = new List<Donation>();   
   

}