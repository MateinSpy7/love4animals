using love4animals.Models;

namespace love4animals.Repositories;

public class PostRepository : IPostRepository
{
    private readonly List<Post> _data = new();

    public IEnumerable<Post> GetAll() => _data;
    
    public Post? GetById(Guid id) => _data.FirstOrDefault(p => p.Id == id);
    
    public void Add(Post post) => _data.Add(post);

    public void Update(Post post)
    {
        var existing = GetById(post.Id);
        if (existing != null)
        {
            existing.Content = post.Content;
            existing.ImageUrl = post.ImageUrl;
            existing.CampaignId = post.CampaignId;
        }
    }

    public void Delete(Guid id) => _data.RemoveAll(p => p.Id == id);
}