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
        private readonly IShipsService _shipstuff;

        public ShipsController (IShipsService shipstuff)
        {
            _shipstuff = shipstuff;
        }

        [HttpGet("GetAll")]

        public ActionResult<List<ShipReadDTO>> GetAll()
        {
            return Ok(_shipstuff.GetAll());
        }

        [HttpGet("GetById/{id}")]

        public ActionResult<ShipReadDTO> GetById(int id)
        {
            ShipReadDTO? shipId = _shipstuff.GetById(id);

            if (shipId == null)
            {
                return NotFound("This Ship Does Not Exist......");
            }

            return Ok(shipId);
        }

        [HttpPost("Create")]
        public ActionResult<ShipReadDTO> Create([FromBody] ShipCreateDTO shipstuff)
        {
            ShipReadDTO? created = _shipstuff.Create(shipstuff);

            if (created == null)
            {
                return BadRequest("There seems to be an error here....");
            }

            return CreatedAtAction(nameof(GetById), new {id = created.Id}, created);
        }

        [HttpPut("Update/{id}")]

        public ActionResult Update(int id, Ship changes)
        {
           ShipReadDTO? existing = _shipstuff.GetById(id);

            if (existing == null)
            {
                return NotFound("This Ship Does Not Exist......");
            }

            bool update = _shipstuff.Update(id, changes);

            if (!update)
            {
                return BadRequest("An error has occured....");
            }

            return NoContent(); 
        }

        [HttpDelete("Delete/{id}")]
        
        public IActionResult Delete(int id)
        {

            if (_shipstuff.GetById(id) == null)
            {
                return NotFound($"No ship was here......");
            }

            _shipstuff.Delete(id);
            return NoContent();
        }

    }
}

       
