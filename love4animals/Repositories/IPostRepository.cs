using love4animals.Models;

namespace love4animals.Repositories;

public interface IPostRepository
{
    IEnumerable<Post> GetAll();
    Post? GetById(Guid id);
    void Add(Post post);
    void Update(Post post);
    void Delete(Guid id);
}