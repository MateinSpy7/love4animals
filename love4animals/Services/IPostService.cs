using love4animals.DTOs;

namespace love4animals.Services;

public interface IPostService
{
    IEnumerable<GetPostDto> GetAll();
    GetPostDto? GetById(Guid id);
    GetPostDto Create(CreatePostDto dto);
    bool Update(Guid id, UpdatePostDto dto);
    bool Delete(Guid id);
}