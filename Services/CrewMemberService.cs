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

            CrewMember created = _repo.Add(member); 
            return ToReadDto(member); 


        }//end of Create

        public bool Edit(int id, CrewMemberCreateDto editMember)
        {
            CrewMember? existing = _repo.GetById(id);
            if(existing is null)
            {
                return false;
            }
            existing.Name = editMember.Name;
            existing.Role = editMember.Role;
            existing.IsOnDuty = editMember.IsOnDuty; 
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
    } //end of class
}//end of namespace