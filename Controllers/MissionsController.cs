
using Microsoft.AspNetCore.Mvc;
using Starbase.StationOps.DTOs;
using Starbase.StationOps.Services;

namespace Starbase.StationOps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionsService _mission;

        public MissionsController(IMissionsService mission)
        {
            _mission = mission;
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
    }
}