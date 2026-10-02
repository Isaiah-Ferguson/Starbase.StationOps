using Microsoft.AspNetCore.Mvc;
using Starbase.StationOps.Models;

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


        [HttpGet("Low-Fuel/{fuelLevel}")]

        public ActionResult<ShipReadDTO?> GetByFuel(int fuelLevel)
        {
            ShipReadDTO? shipFuel = _shipstuff.GetByLowFuel(fuelLevel);
                
            
            if (shipFuel == null)
            {
                return NotFound("Ship fuel level outside of range");
            }

            return Ok(fuelLevel);
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



        [HttpPut("{id}/Refuel")]

        public IActionResult Refuel(int id)
        {
            bool refueled = _shipstuff.Refuel(id);

            if (!refueled)
        {
            return NotFound($"No ship with id {id}.");
        }

        return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        
        public IActionResult Delete(int id)
        {
            MaintenanceTicket shipTicket = 

             if (_shipstuff != null && shipTicket.IsResolved == true)
            {
                return NotFound($"No ship was here......");
            }

            _shipstuff.Delete(id);
            return NoContent();
        }

    }
}

       
