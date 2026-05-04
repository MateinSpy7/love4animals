namespace love4animals.Models;

public class Campaign
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal GoalAmount { get; set; }
    public decimal CurrentAmount { get; set; } = 0;

    public ICollection<Post> Posts { get; set; } = new List<Post>(); // Una campaña puede tener muchos posts
    public ICollection<Donation> Donations { get; set; } = new List<Donation>(); // Una campaña puede tener muchas donaciones   
    public User Creator { get; set; } = null!; // Una campaña tiene un creador, es decir un usuario que la creó

}