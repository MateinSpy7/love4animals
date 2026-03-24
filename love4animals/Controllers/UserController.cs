using Microsoft.AspNetCore.Mvc;
using love4animals.Services;
using love4animals.DTOs;

namespace love4animals.Controllers;

[ApiController]
[Route("v1/users/profile")] 
public class UserController : ControllerBase
{
    private readonly IUserService _svc;

    public UserController(IUserService svc) => _svc = svc;

    [HttpGet]
    public ActionResult<IEnumerable<GetUserDto>> GetAll() => Ok(_svc.GetAll());

    [HttpGet("{id}")]
    public ActionResult<GetUserDto> GetById(Guid id)
    {
        var user = _svc.GetById(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public ActionResult<GetUserDto> Create([FromBody] CreateUserDto dto)
    {
        var created = _svc.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public ActionResult Update(Guid id, [FromBody] UpdateUserDto dto)
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