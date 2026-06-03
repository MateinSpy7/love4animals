using Microsoft.AspNetCore.Mvc;
using love4animals.Services;
using love4animals.DTOs;
using Microsoft.AspNetCore.Authorization; 

namespace love4animals.Controllers;

[ApiController]
[Route("v1/posts")]
[Authorize] 
public class PostController : ControllerBase
{
    private readonly IPostService _svc;

    public PostController(IPostService svc) => _svc = svc;

    //Cualquier logueado ve los posts
    [HttpGet]
    public ActionResult<IEnumerable<GetPostDto>> GetAll() => Ok(_svc.GetAll());

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GetPostDto> GetById(Guid id)
    {
        var post = _svc.GetById(id);
        return post == null ? NotFound() : Ok(post);
    }

    // SOLO MISIONEROS pueden crear
    [HttpPost]
    [Authorize(Roles = "misionero")] 
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<GetPostDto> Create([FromBody] CreatePostDto dto)
    {
        var created = _svc.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "misionero")] 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(Guid id, [FromBody] UpdatePostDto dto)
    {
        var updated = _svc.Update(id, dto);
        return updated ? NoContent() : NotFound();
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = "misionero")] 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(Guid id)
    {
        var deleted = _svc.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}