using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Starbase.StationOps.Dtos;
using Starbase.StationOps.Models;
using Starbase.StationOps.Services;

namespace Starbase.StationOps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DockingBaysController : ControllerBase
    {
        private readonly IDockingBayService _dockingbays;

        public DockingBaysController(IDockingBayService dockingbay)
        {
            _dockingbays = dockingbay;
        }

        [HttpGet]
        public ActionResult<List<DockingBayReadDto>> GetAll()
        {
            return Ok(_dockingbays.GetAll());
        }

        [HttpGet("Available")]
        public ActionResult<List<DockingBayReadDto>> IsBayAvailable()
        {
            List<DockingBayReadDto> openbays = _dockingbays.GetAll();
            return Ok(openbays);
        }


        [HttpGet("{id}")]
        public ActionResult<DockingBayReadDto> GetById(int id)
        {
            DockingBayReadDto? dockingbay = _dockingbays.GetById(id);
            if(dockingbay == null)
            {
                return NotFound($"No docking bay found with ID {id} ");
            }

            return Ok(dockingbay);
        }

        [HttpPost("add")]
        public ActionResult<DockingBayReadDto> Add([FromBody] DockingBayCreateDto dockingBay)
        {
            DockingBayReadDto? created = _dockingbays.Add(dockingBay);

            if(created == null)
            {
                return BadRequest($"Bay Number {dockingBay.BayNumber} already exists.");
            }
            return CreatedAtAction(
                nameof(GetById),
                new{id = created.Id},
                created
                );
        }

        [HttpPatch("{id}")]
        public ActionResult<DockingBay> Update(int id, DockingBay dockingBay)
        {
            DockingBayReadDto? update = _dockingbays.GetById(id);

            if(update == null)
            {
                return NotFound($"No docking bay number with ID {id} found.");
            }

            bool ok = _dockingbays.Update(id, dockingBay);
            if (!ok)
            {
                return BadRequest("That docking bay is already occupied.");
            }

            
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            DockingBayReadDto? deleted = _dockingbays.GetById(id);

            if(deleted == null)
            {
                return NotFound($"No docking bay found with ID {id}");
            }

            _dockingbays.Delete(id);
            return NoContent();
        }


        [HttpPut("{id}/dock/{shipId}")]

        public ActionResult<DockingBayReadDto> Dock(int id, int shipId)

        {
            bool isAvailable = _dockingbays.IsBayAvailable(id);

            if (isAvailable == false || shipId == null)
            {
                return NotFound("This Ship Does Not Exist or this Docking Bay is not Available....");
            }

            isAvailable = true;
            return Ok();

        }

        [HttpPut("{id}/undock/{shipId}")]
  
        public ActionResult<DockingBayReadDto> Undock(int id, int shipId)
        {
            bool isAvailable = _dockingbays.IsBayAvailable(id);

            if (isAvailable == true || shipId == null)
            {
                return NotFound("This Ship Does Not Exist or this Docking Bay is not Available....");
            }

            isAvailable = false;
            return Ok();
        }

        [HttpGet("by-ship/{shipId}")]

        public ActionResult<DockingBayReadDto> Return(int shipId)
        {
            DockingBayReadDto? returnBay = _dockingbays.GetAll().FirstOrDefault(b => b.ShipId == shipId);
            if (returnBay == null)
            {
                return NotFound("No Docking Bay Found for this Ship....");
            }

            return Ok(returnBay); 

            
        }
        



    }
}