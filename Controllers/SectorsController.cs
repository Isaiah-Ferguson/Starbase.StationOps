using Microsoft.AspNetCore.Mvc;
using Starbase.StationOps.Models;
using Starbase.StationOps.Services;

namespace Starbase.StationOps.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectorsController : ControllerBase
{
    private readonly ISectorService _sectors;

    public SectorsController(ISectorService sectors)
    {
        _sectors = sectors;
    }

    [HttpGet]
    public ActionResult<List<Sector>> GetAll()
    {
        return Ok(_sectors.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Sector> GetById(int id)
    {
        Sector? sector = _sectors.GetById(id);

        if (sector == null)
        {
            return NotFound($"No sector with id {id}.");
        }

        return Ok(sector);
    }

    [HttpPost]
    public ActionResult<Sector> Create(Sector sector)
    {
        Sector? created = _sectors.Create(sector);

        if (created == null)
        {
            return BadRequest("A sector needs a name and a security level from 1 to 5.");
        }

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public ActionResult<Sector> Update(int id, Sector changes)
    {
        Sector? existing = _sectors.GetById(id);

        if (existing == null)
        {
            return NotFound($"No sector with id {id}.");
        }

        bool ok = _sectors.Update(existing, changes);

        if (!ok)
        {
            return BadRequest("A sector needs a name and a security level from 1 to 5.");
        }

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Sector? sector = _sectors.GetById(id);

        if (sector == null)
        {
            return NotFound($"No sector with id {id}.");
        }

        _sectors.Delete(sector);
        return NoContent();
    }
}
