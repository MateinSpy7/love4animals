using love4animals.Models;

namespace love4animals.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _data = new(); 

    public IEnumerable<User> GetAll() => _data;

    public User? GetById(Guid id) => _data.FirstOrDefault(u => u.Id == id);

    public void Add(User user) => _data.Add(user);

    public void Update(User user)
    {
        var existingUser = GetById(user.Id);
        if (existingUser != null)
        {
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
        }
    }

    public void Delete(Guid id) => _data.RemoveAll(u => u.Id == id);
}