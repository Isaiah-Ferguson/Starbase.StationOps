
using Microsoft.AspNetCore.Mvc;
using Starbase.StationOps.Dtos;
using Starbase.StationOps.DTOs;
using Starbase.StationOps.Models;
using Starbase.StationOps.Services;

namespace Starbase.StationOps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionsService _mission;
        private readonly IMaintenanceTicketService _maintenance; 

        public MissionsController(IMissionsService mission, IMaintenanceTicketService maintenance)
        {
            _mission = mission;
            _maintenance = maintenance; 
        }

    [HttpGet("GetAll")]
    public ActionResult<List<MissionsReadDTO>> GetAll()
    {
        return Ok(_mission.GetAll());
    }
    [HttpGet("GetById/{id}")]
    public ActionResult<MissionsReadDTO> GetById(int id)
        {
            MissionsReadDTO? mission = _mission.GetById(id);
            if (mission == null)
            {
                return NotFound($"No mission with id {id}.");
            }

            return Ok(mission);
        }

    [HttpPost]
    public ActionResult<MissionsReadDTO> Create([FromBody] MissionsCreateDTO mission)
    {
        MissionsReadDTO? created = _mission.Create(mission);

        if (created == null)
        {
            return BadRequest("Missions need a Title with fewer than 60 characters, a Ship Id, and a Status of 'Planned', 'Active', or 'Complete'. ");
        }
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("Update/{id}")]
    public ActionResult<MissionsReadDTO> Update(int id, MissionsCreateDTO dto)
    {
        bool updated = _mission.Update(id, dto);

        if (!updated)
        {
            MissionsReadDTO? existingMission = _mission.GetById(id);
            if (existingMission is null)
            {
                return NotFound($"No mission with id {id}.");
            }

            if (string.Equals(existingMission.Status, "Complete", StringComparison.OrdinalIgnoreCase))
            {
                return Conflict("Completed missions cannot be edited.");
            }

            return BadRequest("Invalid mission data.");
        }

        return Ok(updated);
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        MissionsReadDTO? mission = _mission.GetById(id);

        if (mission == null)
        {
            return NotFound($"No mission with id {id}.");
        }

        _mission.Delete(mission.Id);
        return NoContent();
    }

    [HttpPut("{id}/advance")]
    public ActionResult Advance(int id)
        {
            MissionsReadDTO? mission = _mission.GetById(id);
            if (mission is null)
            {
                return NotFound($"No mission with id {id}.");
            }

            if (string.Equals(mission.Status, "Complete", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Completed missions cannot be advanced");
            }
            MaintenanceTicketReadDto? resolved = _maintenance.GetAll().FirstOrDefault(s => s.ShipId == mission.ShipId); 
            if(resolved != null)
            {
               if(resolved.IsResolved == false)
            {
                return BadRequest("Must Resolve Maintance Ticket before advancing the mission");
            }
            }
            
           
            _mission.Advance(id);
          
            return Ok(_mission.GetById(id).Status); 
        }
        [HttpGet("by-ship/{shipId}")]
        public ActionResult<List<MissionsReadDTO>>? GetMission(int shipId)
        {
             List<MissionsReadDTO>? ship = _mission.GetMissions(shipId);
            if (ship == null)
            {
                return NotFound($"No ship with id {shipId}.");
            }
          
            return Ok(ship);  
        }
    }
}