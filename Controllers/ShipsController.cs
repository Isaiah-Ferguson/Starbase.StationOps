using Microsoft.AspNetCore.Mvc;
using Starbase.StationOps.Models;
using Starbase.StationOps.Services;

namespace Starbase.StationOps
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipsController : ControllerBase
    {
        private readonly IShipsService _shipstuff;

        private readonly IMaintenanceTicketService _shipticket;

        public ShipsController (IShipsService shipstuff, IMaintenanceTicketService shipticket)
        {
            _shipstuff = shipstuff;

            _shipticket = shipticket;
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


        [HttpGet("Low-Fuel/{threshhold}")]

        public ActionResult<List<ShipReadDTO>> GetByFuel(int threshhold)
        {
            if(threshhold > 100 && threshhold < 0)
            {
                return BadRequest("Threshold is outside range");
            }
            List<ShipReadDTO>? shipFuel = _shipstuff.GetByLowFuel(threshhold);
                
            
            if (shipFuel == null)
            {
                return NotFound("Ship fuel level outside of range");
            }

            return Ok(shipFuel);
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
            

             if (_shipticket.GetByShipId(id) != null)
            {
                return NotFound($"No ship was here......");
            }

            _shipstuff.Delete(id);
            return NoContent();
        }

    }
}

       
