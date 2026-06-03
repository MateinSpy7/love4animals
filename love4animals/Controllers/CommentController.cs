using Microsoft.AspNetCore.Mvc;
using love4animals.Services;
using love4animals.DTOs;
using Microsoft.AspNetCore.Authorization; // <-- NUEVO

namespace love4animals.Controllers;

[ApiController]
[Route("v1/posts/{postId}/comments")] 
[Authorize] // <-- Candado general para requerir sesión
public class CommentController : ControllerBase
{
    private readonly ICommentService _svc;

    public CommentController(ICommentService svc) => _svc = svc;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<GetCommentDto>> GetAll(Guid postId) 
        => Ok(_svc.GetAllByPostId(postId)); 

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    public ActionResult<GetCommentDto> Create(Guid postId, [FromBody] CreateCommentDto dto)
    {
        var created = _svc.Create(postId, dto); 
        return CreatedAtAction(nameof(GetAll), new { postId = postId }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GetCommentDto> Update(Guid postId, Guid id, [FromBody] UpdateCommentDto dto)
    {
        var updated = _svc.Update(id, dto);
        return updated != null ? Ok(updated) : NotFound();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)] 
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    public ActionResult Delete(Guid postId, Guid id)
    {
        var deleted = _svc.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}