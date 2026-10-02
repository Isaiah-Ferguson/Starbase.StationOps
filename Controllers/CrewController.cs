using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Starbase.StationOps.Dtos;
using Starbase.StationOps.Services;

namespace Starbase.StationOps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrewController : ControllerBase
    {
        private readonly ICrewMemberService _members;
        public CrewController(ICrewMemberService members)
        {
            _members = members; 
        }//end of contructor
        [HttpGet("GetAll")]
        public ActionResult<List<CrewMemberReadDto>> GetAll()
        {
            return Ok(_members.GetAll()); 
        }//end GetAll
        [HttpGet("GetById/{id}")]
        public ActionResult<CrewMemberReadDto> GetById(int id){
            CrewMemberReadDto? member = _members.GetById(id); 
            if(member is null)
            {
                return NotFound($"No Crew Member with id {id} was found");
            }
            return Ok(member); 
        } //end of GetById

        [HttpPost("AddMember")]
        public ActionResult<CrewMemberReadDto> Create([FromBody] CrewMemberCreateDto member)
        {
            CrewMemberReadDto? created = _members.Create(member);
            if(created is null)
            {
                return Conflict($"There is already a Crew Member named {member.Name}");
            }
            return CreatedAtAction(nameof(GetById), new {id = created.Id}, created); 
        }//end of create

        [HttpPut("{id}/edit")]
        public ActionResult<CrewMemberReadDto> Edit(int id, [FromBody] CrewMemberReadDto member)
        {
            CrewMemberReadDto? exists = _members.GetById(id);
            if(exists is null)
            {
                return NotFound($"No Member with id {id} was found");

            }
           bool ok = _members.Edit(id, member);
            if (!ok)
            {
                return BadRequest("A Crew Member requires a Name, Rank, and a Ship Id greater than 0");
            }
           return Ok(ok); 
        } //end of edit

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if(_members.GetById(id) is null)
            {
                return NotFound($"No member with id {id} was found");
            }
            _members.Delete(id);
            return NoContent(); 
        }//end of delete



        [HttpPut("{id}/clock-in")]
        public ActionResult<CrewMemberReadDto> ClockIn(int id)
        {
            CrewMemberReadDto? exists = _members.GetById(id);
            if(exists is null)
            {
                return NotFound($"No Member with id {id} was found");
            }

            bool IsTrue = _members.ClockIn(id);
            if(IsTrue)
            {
                return Ok($"ID {id} is now clocked in");
            }
            else
            {
                return BadRequest($"ID {id} is already clocked in");
            }
        } //end of edit

        [HttpPut("{id}/clock-out")]
        public ActionResult<CrewMemberReadDto> ClockOut(int id)
        {
            CrewMemberReadDto? exists = _members.GetById(id);
            if(exists is null)
            {
                return NotFound($"No Member with id {id} was found");
            }

            bool IsTrue = _members.ClockOut(id);
            if(IsTrue)
            {
                return Ok($"ID {id} is now clocked out");
            }
            else
            {
                return BadRequest($"ID {id} is already clocked out");
            }
        } //end of edit


        [HttpGet("GetByRole/{role}")]
        public ActionResult<List<CrewMemberReadDto>> GetByRole(string role)
        {
            List<CrewMemberReadDto> member = _members.GetByRole(role); 
            if(member.Count == 0)
            {
                return BadRequest($"No ID with the role exists");
            }
            else
            {
                return Ok(_members.GetByRole(role)); 
            }
        }//end GetAll

        [HttpPut("{id}/assign/{shipid}")]
        public ActionResult<CrewMemberReadDto> AssignShipId(int id, int shipid)
        {
            CrewMemberReadDto? exists = _members.GetById(id);
            if(exists is null)
            {
                return NotFound($"No crew member with id {id} was found");
            }

            bool ok = _members.AssignShipId(id, shipid);
            
            if(ok == true)
            {
                return Ok($"ID {id} has been assigned to {shipid}");
            }
            else
            {
                return BadRequest($"No ship with id {shipid} was found");
            }
            // assigns a crew member to a ship.


            //404 if the CREW member doesn't exist


            // 400 if the SHIP doesn't exist
        }
    } //end of class
}//end of namespace