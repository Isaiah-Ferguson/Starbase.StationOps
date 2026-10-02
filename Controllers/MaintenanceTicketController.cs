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
    private readonly IShipsService _ships;

    public MaintenanceTicketController(IMaintenanceTicketService tickets, IShipsService ships)
    {
        _tickets = tickets;
        _ships = ships;
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
            return NotFound($"No ship with id {id}.");
        }

        return Ok(ticket);
    }

    [HttpGet("by-ship/{shipId}")]
    public ActionResult<List<MaintenanceTicketReadDto>> GetByShipId(int shipId)
    {
        if (_ships.GetById(shipId) is null)
        {
            return NotFound($"No ship with id {shipId}.");
        }

        return Ok(_tickets.GetByShipId(shipId));
    }

    [HttpPost]
    public ActionResult<MaintenanceTicketReadDto> Create([FromBody] MaintenanceTicketCreateDto ticket)
    {
        MaintenanceTicketReadDto? created = _tickets.Create(ticket);

        if (created == null)
        {
            return BadRequest("A maintenance ticket needs a valid ship ID and description.");// new error message for not a real ship
        }

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public ActionResult<MaintenanceTicketReadDto> Update(int id, MaintenanceTicketCreateDto changes)
    {
        MaintenanceTicketReadDto? existing = _tickets.GetById(id);

        if (existing == null)
        {
            return NotFound($"No ticket with id {id}.");
        }

        if (!_tickets.Update(id, changes))
        {
            return BadRequest("A maintenance ticket needs a valid ship ID and description.");
        }

        MaintenanceTicketReadDto? updated = _tickets.GetById(id);

        if (updated == null)
        {
            return NotFound($"No ticket with id {id}.");
        }

        return Ok(updated);
    }

    [HttpPut("{id}/resolve")]
    public ActionResult<MaintenanceTicketReadDto> Resolve(int id, MaintenanceTicketCreateDto changes)
    {
        MaintenanceTicketReadDto? existing = _tickets.GetById(id);

        if (existing == null)
        {
            return NotFound($"No maintenance ticket with id {id}.");
        }

        MaintenanceTicketReadDto? resolved = _tickets.Resolve(id, changes);

        if (resolved == null)
        {
            return BadRequest("This ticket is already resolved.");
        }
        existing.ShipId = changes.ShipId;
        existing.Description = changes.Description;

        return Ok(_tickets.GetById(id));
    }

    [HttpPut("{id}/reopen")]
    public ActionResult<MaintenanceTicketReadDto> Reopen(int id, MaintenanceTicketCreateDto changes)
    {
        MaintenanceTicketReadDto? existing = _tickets.GetById(id);

        if (existing == null)
        {
            return NotFound($"No maintenance ticket with id {id}.");
        }

        MaintenanceTicketReadDto? reopened = _tickets.Reopen(id, changes);

        if (reopened == null)
        {
            return BadRequest("This ticket is already open.");
        }
        existing.ShipId = changes.ShipId;
        existing.Description = changes.Description;
        return Ok(reopened);
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
