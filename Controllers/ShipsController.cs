using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Starbase.StationOps
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipsController : ControllerBase
    {
        private readonly IShipsService _ship;

        public ShipsController (IShipsService ship)
        {
            _ship = ship;
        }

        [HttpGet("GetAll")]

        public ActionResult<List<Ship>> GetAll()
        {
            return Ok(_ship.GetAll());
        }

        [HttpGet("GetById/{id}")]

        public ActionResult<ShipReadDTO> GetById(int id)
        {
            ShipReadDTO? shipId = _ship.GetById(id);

            if (shipId == null)
            {
                return NotFound("This Ship Does Not Exist......");
            }

            return Ok(shipId);
        }

        [HttpPost("Create")]
        public ActionResult<ShipReadDTO> Create([FromBody] ShipCreateDTO ship)
        {
            ShipReadDTO? created = _ship.Create(ship);

            if (created == null)
            {
                return BadRequest("There seems to be an error here....");
            }

            return CreatedAtAction(nameof(GetById), new {id = created.Id}, created);
        }

        [HttpPut("Update/{id}")]

        public ActionResult Update(int id, Ship changes)
        {
           ShipReadDTO? existing = _ship.GetById(id);

            if (existing == null)
            {
                return NotFound("This Ship Does Not Exist......");
            }

            bool update = _ship.Update(id, changes);

            if (!update)
            {
                return BadRequest("An error has occured....");
            }

            return NoContent(); 
        }

        [HttpDelete("Delete/{id}")]
        
        public IActionResult Delete(int id)
        {

            if (_ship.GetById(id) == null)
            {
                return NotFound($"No ship was here......");
            }

            _ship.Delete(id);
            return NoContent();
        }

    }
}

       
