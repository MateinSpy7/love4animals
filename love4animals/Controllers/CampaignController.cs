using Microsoft.AspNetCore.Mvc;
using love4animals.Services;
using love4animals.DTOs;

namespace love4animals.Controllers;

[ApiController]
[Route("v1/campaigns")]
public class CampaignController : ControllerBase
{
    private readonly ICampaignService _svc;

    public CampaignController(ICampaignService svc) => _svc = svc;

    [HttpGet]
    public ActionResult<IEnumerable<GetCampaignDto>> GetAll() => Ok(_svc.GetAll());

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)] // Respuesta exitosa
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Respuesta si no existe
    public ActionResult<GetCampaignDto> GetById(Guid id)
    {
        var campaign = _svc.GetById(id);
        return campaign == null ? NotFound() : Ok(campaign);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)] // Respuesta cuando se crea un recurso
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Respuesta si los datos son inválidos

    public ActionResult<GetCampaignDto> Create([FromBody] CreateCampaignDto dto)
    {
        var created = _svc.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)] // Respuesta si se actualiza correctamente
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Respuesta si no existe

    public ActionResult Update(Guid id, [FromBody] UpdateCampaignDto dto)
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