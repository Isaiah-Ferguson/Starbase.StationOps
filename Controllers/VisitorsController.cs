using Microsoft.AspNetCore.Mvc;
using Starbase.StationOps.DTOs;
using Starbase.StationOps.Models;
using Starbase.StationOps.Services;

namespace Starbase.StationOps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitorsController : ControllerBase
    {
        private readonly IVisitorServices _visitors;

        public VisitorsController(IVisitorServices visitors)
        {
            _visitors = visitors;
        }

        [HttpGet]

        public ActionResult <List<VisitorReadDTO>> GetAll()
        {
            return Ok(_visitors.GetAll());
        }

        [HttpGet("{id}")]

        public ActionResult<VisitorReadDTO> GetById(int id)
        {
            VisitorReadDTO? visitor = _visitors.GetById(id);

            if(visitor == null)
            {
                return NotFound($"No visitor with Id {id}.");
            }

            return Ok(visitor);
        }

        [HttpPost]

        public ActionResult<VisitorReadDTO> Create([FromBody] VisitorCreateDTO newVisitor)
        {
            VisitorReadDTO? created = _visitors.Create(newVisitor);

            if(created == null)
            {
                return BadRequest($"A visitor needs a name to be added");
            }
            return CreatedAtAction(nameof(GetById), new {id = created.Id}, created);
        }


        [HttpPut("{id}")]

        public ActionResult<VisitorReadDTO> Update(int id, Visitors changes)
        {
            VisitorReadDTO? existing = _visitors.GetById(id);

            if (existing == null)
            {
                return NotFound($"No visitor found with id {id}.");
            }

            bool ok = _visitors.Update(id, changes);

            if (!ok)
            {
                return BadRequest($"A visitor needs a name to be registered.");
            }

            return Ok(existing);
        }


        [HttpDelete("{id}")]

        public IActionResult  Delete(int id)
        {
            VisitorReadDTO? visitor = _visitors.GetById(id);

            if (visitor == null)
            {
                return NotFound($"No visitor found with id {id}");
            }

            _visitors.Delete(id);

            return NoContent();
        }
    }
}