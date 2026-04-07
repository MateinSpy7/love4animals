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
    [ProducesResponseType(StatusCodes.Status200OK)] // Respuesta exitosa
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Respuesta si no existe
    public ActionResult<GetUserDto> GetById(Guid id)
    {
        var user = _svc.GetById(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)] // Respuesta cuando se crea un recurso
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Respuesta si los datos son inválidos

    public ActionResult<GetUserDto> Create([FromBody] CreateUserDto dto)
    {
        var created = _svc.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)] // Respuesta si se actualiza correctamente
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Respuesta si no existe

    public ActionResult Update(Guid id, [FromBody] UpdateUserDto dto)
    {
        var updated = _svc.Update(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)] // Respuesta si se elimina correctamente
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Respuesta si no existe
    public ActionResult Delete(Guid id)
    {
        var deleted = _svc.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}