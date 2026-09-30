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
            ShipCreateDTO? created = _ship.Create(ship);

            if (created == null)
            {
                return BadRequest("There seems to be an error here....");
            }

            return CreatedAtAction(nameof(GetById), new {id = created.Id}, created);
        }

    }
}