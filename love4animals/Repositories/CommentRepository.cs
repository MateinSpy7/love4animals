using love4animals.Models;
using Microsoft.EntityFrameworkCore;
using love4animals.Data;

namespace love4animals.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly Love4AnimalsDbContext _context;

    public CommentRepository(Love4AnimalsDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Comment> GetByPostId(Guid postId)
    {
        return _context.Comments.Where(c => c.PostId == postId).ToList();
    }

    public Comment? GetById(Guid id)
    {
        return _context.Comments.FirstOrDefault(c => c.Id == id);
    }

    public void Add(Comment comment)
    {
        _context.Comments.Add(comment);
        _context.SaveChanges();
    }

    public void Update(Comment comment)
    {
        var existing = GetById(comment.Id);
        if (existing != null)
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();
        }
    }

    public void Delete(Guid id)
    {
        var comment = GetById(id);
        if (comment != null)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges(); 
    }
}
}