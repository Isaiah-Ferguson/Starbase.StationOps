using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Starbase.StationOps.Dtos;
using Starbase.StationOps.Models;
using Starbase.StationOps.Repositories;

namespace Starbase.StationOps.Services
{
    public class CrewMemberService : ICrewMemberService
    {


        private readonly ICrewMemberRepository _repo; 

        public CrewMemberService(ICrewMemberRepository repo)
        {
            _repo = repo; 
        }//end of constructor

        public List<CrewMemberReadDto> GetAll()
        {
            return _repo.GetAll()
            .OrderBy(c => c.Name)
            .Select(c => ToReadDto(c))
            .ToList(); 
        } //end of GetAll
        public CrewMemberReadDto? GetById(int id)
        {
            CrewMember? member = _repo.GetById(id);
            if(member is null)
            {
                return null;
            }
            return ToReadDto(member); 

        } //end of GetById
        public CrewMemberReadDto? Create(CrewMemberCreateDto newCrewMember)
        {
            bool exists = _repo.GetAll().Any(s => s.Name.ToLower() == newCrewMember.Name.ToLower());
            if (exists)
            {
                return null;
            }
            CrewMember member = new CrewMember(); 

            member.Name = newCrewMember.Name;
            member.Role = newCrewMember.Role; 
            member.IsOnDuty = newCrewMember.IsOnDuty; 
            member.ShipId = 0; 
            CrewMember created = _repo.Add(member); 
            return ToReadDto(member); 


        }//end of Create

        public bool Edit(int id, CrewMemberReadDto editMember)
        {
            CrewMember? existing = _repo.GetById(id);
            if(existing is null || !isValid(editMember))
            {
                return false;
            }
            existing.Name = editMember.Name;
            existing.Role = editMember.Role;
            existing.IsOnDuty = editMember.IsOnDuty;
            existing.ShipId = editMember.ShipId;  
            _repo.Update(existing);
            return true; 
        } //end of Edit
        public void Delete(int id)
        {
            CrewMember? member = _repo.GetById(id);
            if(member != null)
            {
                _repo.Delete(member); 
            }
        }//end of Delete
        
        private static CrewMemberReadDto ToReadDto(CrewMember member)
        {
            CrewMemberReadDto outputDto = new CrewMemberReadDto(); 
            outputDto.Id = member.Id; 
            outputDto.Name = member.Name; 
            outputDto.Role = member.Role; 
            outputDto.IsOnDuty = member.IsOnDuty; 
            outputDto.ShipId = member.ShipId; 

            return outputDto; 
        }//end of ToReadDto
        private bool isValid(CrewMemberReadDto member)
        {
            if (string.IsNullOrWhiteSpace(member.Name))
            {
                return false; 
            }
             if (string.IsNullOrWhiteSpace(member.Role))
            {
                return false; 
            }
            if(member.ShipId < 0)
            {
                return false; 
            }

            return true; 
        }
    } //end of class
}//end of namespace