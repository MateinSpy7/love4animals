using love4animals.Models;
using Microsoft.EntityFrameworkCore;    
using love4animals.Data;



namespace love4animals.Repositories;

public class UserRepository : IUserRepository
{
    private readonly Love4AnimalsDbContext _context; 

    public UserRepository(Love4AnimalsDbContext context)
    {
        _context = context;
    }
    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User? GetById(Guid id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Update(User user)
    {
        var existingUser = GetById(user.Id);
        if (existingUser != null)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }

    public void Delete(Guid id)
    {
        var user = GetById(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}