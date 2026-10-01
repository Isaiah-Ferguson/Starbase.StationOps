using Microsoft.AspNetCore.Mvc;
using Starbase.StationOps.DTO;
using Starbase.StationOps.Dtos;
using Starbase.StationOps.Models;
using Starbase.StationOps.Services;

namespace Starbase.StationOps.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceTicketController : ControllerBase
{
    private readonly IMaintenanceTicketService _tickets;

    public MaintenanceTicketController(IMaintenanceTicketService tickets)
    {
        _tickets = tickets;
    }

    [HttpGet]
    public ActionResult<List<MaintenanceTicket>> GetAll()
    {
        return Ok(_tickets.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<MaintenanceTicket> GetById(int id)
    {
        MaintenanceTicketReadDto? ticket = _tickets.GetById(id);

        if (ticket == null)
        {
            return NotFound($"No sector with id {id}.");
        }

        return Ok(ticket);
    }

    [HttpPost]
    public ActionResult<MaintenanceTicketReadDto> Create ([FromBody] MaintenanceTicketCreateDto ticket)
    {
        MaintenanceTicketReadDto? created = _tickets.Create(ticket);

        if (created == null)
        {
            return BadRequest("A sector needs a name and a security level from 1 to 5.");
        }

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public ActionResult<MaintenanceTicketReadDto> Update(int id, MaintenanceTicketCreateDto changes)
    {
        MaintenanceTicketReadDto? existing = _tickets.GetById(id);

        if (existing == null)
        {
            return NotFound($"No sector with id {id}.");
        }

        bool ok = _tickets.Update(existing.Id, changes);

        if (!ok)
        {
            return BadRequest("A sector needs a name and a security level from 1 to 5.");
        }

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        MaintenanceTicketReadDto? ticket = _tickets.GetById(id);

        if (ticket == null)
        {
            return NotFound($"No sector with id {id}.");
        }

        _tickets.Delete(ticket.Id);
        return NoContent();
    }
}
