using love4animals.DTOs;

namespace love4animals.Services;

public interface IUserService
{
    IEnumerable<GetUserDto> GetAll();
    GetUserDto? GetById(Guid id);
    GetUserDto Create(CreateUserDto dto);
    bool Update(Guid id, UpdateUserDto dto);
    bool Delete(Guid id);
}