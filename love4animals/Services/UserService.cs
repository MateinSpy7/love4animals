using love4animals.DTOs;
using love4animals.Repositories;
using love4animals.Models;

namespace love4animals.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo) => _repo = repo;

    public IEnumerable<GetUserDto> GetAll()
    {
        return _repo.GetAll().Select(u => new GetUserDto(u.Id, u.Name, u.Email));
    }

    public GetUserDto? GetById(Guid id)
    {
        var user = _repo.GetById(id);
        return user == null ? null : new GetUserDto(user.Id, user.Name, user.Email);
    }

    public GetUserDto Create(CreateUserDto dto)
    {
        var user = new User { Name = dto.Name, Email = dto.Email };
        _repo.Add(user);
        return new GetUserDto(user.Id, user.Name, user.Email);
    }

    public bool Update(Guid id, UpdateUserDto dto)
    {
        var existingUser = _repo.GetById(id);
        if (existingUser == null) return false;

        existingUser.Name = dto.Name;
        existingUser.Email = dto.Email;
        _repo.Update(existingUser);
        return true;
    }

    public bool Delete(Guid id)
    {
        var existingUser = _repo.GetById(id);
        if (existingUser == null) return false;

        _repo.Delete(id);
        return true;
    }
}