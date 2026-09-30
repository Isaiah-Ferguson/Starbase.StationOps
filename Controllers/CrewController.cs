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
        }
        [HttpGet("GetById/{id}")]
        public ActionResult<CrewMemberReadDto> GetById(int id){
            CrewMemberReadDto? member = _members.GetById(id); 
            if(member is null)
            {
                return NotFound($"No Crew Member with id {id} was found");
            }
            return Ok(member); 
        }

        [HttpPost("AddMember")]
        public ActionResult<CrewMemberReadDto> Create([FromBody] CrewMemberCreateDto member)
        {
            CrewMemberReadDto? created = _members.Create(member);
            if(created is null)
            {
                return Conflict($"There is already a Crew Member named {member.Name}");
            }
            return CreatedAtAction(nameof(GetById), new {id = created.Id}, created); 
        }

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
        }

        
    } //end of class
}//end of namespace