using love4animals.Models;
using love4animals.Data;
using Microsoft.EntityFrameworkCore;

namespace love4animals.Repositories;

public class PostRepository : IPostRepository
{
    private readonly Love4AnimalsDbContext _context;

    public PostRepository(Love4AnimalsDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Post> GetAll()
    {
        return _context.Posts.ToList();
    }
    
    public Post? GetById(Guid id)
    {
        return _context.Posts.FirstOrDefault(p => p.Id == id);
    }
    
    public void Add(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChanges();
    }

    public void Update(Post post)
    {
        var existing = GetById(post.Id);
        if (existing != null)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
    }

    public void Delete(Guid id)
    {
        var post = GetById(id);
        if (post != null)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}