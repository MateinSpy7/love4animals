using love4animals.DTOs;

namespace love4animals.Services;

public interface ICommentService
{
    IEnumerable<GetCommentDto> GetAllByPostId(Guid postId);
    GetCommentDto? GetById(Guid id);

    
    //Recibe el postId de la URL y los datos del body (dto)
    GetCommentDto Create(Guid postId, CreateCommentDto dto);
    bool Update(Guid id, UpdateCommentDto dto);
    bool Delete(Guid id);
}