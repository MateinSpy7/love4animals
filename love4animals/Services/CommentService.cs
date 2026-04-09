using love4animals.DTOs;
using love4animals.Models;
using love4animals.Repositories;

namespace love4animals.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repo;

    public CommentService(ICommentRepository repo) => _repo = repo;

    public IEnumerable<GetCommentDto> GetAllByPostId(Guid postId) =>
        _repo.GetByPostId(postId).Select(c => new GetCommentDto(c.Id, c.Text, c.CreatedAt, c.PostId, c.UserId));

    public GetCommentDto? GetById(Guid id)
    {
        var comment = _repo.GetById(id);
        return comment == null ? null : new GetCommentDto(comment.Id, comment.Text, comment.CreatedAt, comment.PostId, comment.UserId);
    }

    public GetCommentDto Create(Guid postId, CreateCommentDto dto)
    {
        var comment = new Comment
        {
            Text = dto.Text,
            PostId = postId, //asignakos al post que corresponde
            UserId = dto.UserId
        };
        
        _repo.Add(comment); //id y fecha automaticos
        
        return new GetCommentDto(comment.Id, comment.Text, comment.CreatedAt, comment.PostId, comment.UserId);
    }

    /*public bool Update(Guid id, UpdateCommentDto dto)
    {
        var existing = _repo.GetById(id);
        if (existing == null) return false;

        existing.Text = dto.Text;
        _repo.Update(existing);
        return true;
    }*/
    public GetCommentDto? Update(Guid id, UpdateCommentDto dto)
{
    var existing = _repo.GetById(id);
    if (existing == null) return null;

    existing.Text = dto.Text;
    _repo.Update(existing);
    
    // Devolvemos el objeto actualizado convertido en DTO
    return new GetCommentDto(existing.Id, existing.Text, existing.CreatedAt, existing.PostId, existing.UserId);
}

    public bool Delete(Guid id)
    {
        if (_repo.GetById(id) == null) return false;
        _repo.Delete(id);
        return true;
    }
}