using Microsoft.AspNetCore.Mvc;
using love4animals.Services;
using love4animals.DTOs;

namespace love4animals.Controllers;

[ApiController]
[Route("v1/donations")] 
public class DonationController : ControllerBase
{
    private readonly IDonationService _svc;

    public DonationController(IDonationService svc) => _svc = svc;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<GetDonationDto>> GetAll() => Ok(_svc.GetAll());

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GetDonationDto> GetById(Guid id)
    {
        var donation = _svc.GetById(id);
        return donation != null ? Ok(donation) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<GetDonationDto> Create([FromBody] CreateDonationDto dto)
    {
        var created = _svc.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GetDonationDto> Update(Guid id, [FromBody] UpdateDonationDto dto)
    {
        var updated = _svc.Update(id, dto);
        return updated != null ? Ok(updated) : NotFound();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(Guid id)
    {
        return _svc.Delete(id) ? NoContent() : NotFound();
    }
}