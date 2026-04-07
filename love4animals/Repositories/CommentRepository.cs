using love4animals.Models;

namespace love4animals.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly List<Comment> _data = new();

    //coincidencia de posid con el postId del comentario

    public IEnumerable<Comment> GetByPostId(Guid postId) => _data.Where(c => c.PostId == postId);

    public Comment? GetById(Guid id) => _data.FirstOrDefault(c => c.Id == id);

    public void Add(Comment comment) => _data.Add(comment);

    public void Update(Comment comment)
    {
        var existing = GetById(comment.Id);
        if (existing != null)
        {
            // Solo puede cam,biar el texto del comentrio
            existing.Text = comment.Text; 
        }
    }

    public void Delete(Guid id) => _data.RemoveAll(c => c.Id == id);
}