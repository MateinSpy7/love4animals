using Microsoft.AspNetCore.Mvc;
using love4animals.Services;
using love4animals.DTOs;

namespace love4animals.Controllers;

[ApiController]
[Route("v1/posts/{postId}/comments")] 
public class CommentController : ControllerBase
{
    private readonly ICommentService _svc;

    public CommentController(ICommentService svc) => _svc = svc;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)] // Respuesta exitosa
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Respuesta si no existe

    public ActionResult<IEnumerable<GetCommentDto>> GetAll(Guid postId) 
        => Ok(_svc.GetAllByPostId(postId)); // Tu servicio deberá filtrar por postId

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)] // Respuesta cuando se crea un recurso
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Respuesta si los datos son
    public ActionResult<GetCommentDto> Create(Guid postId, [FromBody] CreateCommentDto dto)
    {
        var created = _svc.Create(postId, dto); // Pásale el postId al servicio
        return CreatedAtAction(nameof(GetAll), new { postId = postId }, created);
    }
    
    /*[HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)] // Respuesta si se actualiza correctamente
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Respuesta si no existe

    public ActionResult Update(Guid postId, Guid id, [FromBody] UpdateCommentDto dto)
    {
        var updated = _svc.Update(id, dto);
        return updated ? NoContent() : NotFound();
    }
*/

[HttpPut("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public ActionResult<GetCommentDto> Update(Guid postId, Guid id, [FromBody] UpdateCommentDto dto)
{
    var updated = _svc.Update(id, dto);
    // Si updated no es null, devolvemos 200 OK con los datos. Si es null, 404.
    return updated != null ? Ok(updated) : NotFound();
}


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)] // Respuesta si se elimina correctamente
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Respuesta si no existe
    public ActionResult Delete(Guid postId, Guid id)
    {
        var deleted = _svc.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
    
}