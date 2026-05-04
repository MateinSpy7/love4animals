namespace love4animals.Models;

public class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

 public ICollection<Post> Posts { get; set; } = new List<Post>();   
 public ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();   
public ICollection<Comment> Comments { get; set; } = new List<Comment>();

}