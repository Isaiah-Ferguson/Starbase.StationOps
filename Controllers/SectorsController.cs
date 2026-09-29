using Microsoft.AspNetCore.Mvc;
using Starbase.StationOps.Models;
using Starbase.StationOps.Services;

namespace Starbase.StationOps.Controllers;

// EXAMPLE — the five endpoints every resource needs. Copy this pattern.
[ApiController]
[Route("api/[controller]")]          // -> /api/sectors
public class SectorsController : ControllerBase
{
    private readonly ISectorService _sectors;

    public SectorsController(ISectorService sectors)
    {
        _sectors = sectors;
    }

    // GET /api/sectors
    [HttpGet]
    public ActionResult<List<Sector>> GetAll()
    {
        return Ok(_sectors.GetAll());                               // 200
    }

    // GET /api/sectors/1
    [HttpGet("{id}")]
    public ActionResult<Sector> GetById(int id)
    {
        Sector? sector = _sectors.GetById(id);

        if (sector == null)
        {
            return NotFound($"No sector with id {id}.");            // 404
        }

        return Ok(sector);                                          // 200
    }

    // POST /api/sectors
    [HttpPost]
    public ActionResult<Sector> Create(Sector sector)
    {
        Sector? created = _sectors.Create(sector);

        if (created == null)
        {
            return BadRequest("A sector needs a name and a security level from 1 to 5.");   // 400
        }

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);         // 201
    }

    // PUT /api/sectors/1
    [HttpPut("{id}")]
    public ActionResult<Sector> Update(int id, Sector changes)
    {
        Sector? existing = _sectors.GetById(id);

        if (existing == null)
        {
            return NotFound($"No sector with id {id}.");            // 404
        }

        bool ok = _sectors.Update(existing, changes);

        if (!ok)
        {
            return BadRequest("A sector needs a name and a security level from 1 to 5.");   // 400
        }

        return Ok(existing);                                        // 200
    }

    // DELETE /api/sectors/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Sector? sector = _sectors.GetById(id);

        if (sector == null)
        {
            return NotFound($"No sector with id {id}.");            // 404
        }

        _sectors.Delete(sector);
        return NoContent();                                         // 204
    }
}
