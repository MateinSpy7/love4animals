using Microsoft.AspNetCore.Mvc;
using love4animals.Services;
using love4animals.DTOs;

namespace love4animals.Controllers;

[ApiController]
[Route("v1/posts")] // Ruta correcta para los posts
public class PostController : ControllerBase
{
    private readonly IPostService _svc;

    public PostController(IPostService svc) => _svc = svc;

    [HttpGet]
    public ActionResult<IEnumerable<GetPostDto>> GetAll() => Ok(_svc.GetAll());

    [HttpGet("{id}")]
    public ActionResult<GetPostDto> GetById(Guid id)
    {
        var post = _svc.GetById(id);
        return post == null ? NotFound() : Ok(post);
    }

    [HttpPost]
    public ActionResult<GetPostDto> Create([FromBody] CreatePostDto dto)
    {
        var created = _svc.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public ActionResult Update(Guid id, [FromBody] UpdatePostDto dto)
    {
        var updated = _svc.Update(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var deleted = _svc.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}