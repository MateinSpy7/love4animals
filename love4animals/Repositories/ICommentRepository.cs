using love4animals.Models;

namespace love4animals.Repositories;

public interface ICommentRepository
{
    //filtramos por el Id del post
    IEnumerable<Comment> GetByPostId(Guid postId); 
    Comment? GetById(Guid id);
    void Add(Comment comment);
    void Update(Comment comment);
    void Delete(Guid id);
}