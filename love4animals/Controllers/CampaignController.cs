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
    public ActionResult<GetCampaignDto> GetById(Guid id)
    {
        var campaign = _svc.GetById(id);
        return campaign == null ? NotFound() : Ok(campaign);
    }

    [HttpPost]
    public ActionResult<GetCampaignDto> Create([FromBody] CreateCampaignDto dto)
    {
        var created = _svc.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public ActionResult Update(Guid id, [FromBody] UpdateCampaignDto dto)
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