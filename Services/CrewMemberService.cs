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
        private readonly IShipsRepository _repoShip; 

        public CrewMemberService(ICrewMemberRepository repo, IShipsRepository repoShip)
        {
            _repo = repo; 
            _repoShip = repoShip;
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
            return ToReadDto(created); 


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


        public bool ClockIn(int id)
        {
            CrewMember? existing = _repo.GetById(id);
            if (existing.IsOnDuty == false)
            {
                existing.IsOnDuty = true;
                _repo.Update(existing);
                return true;
            }
            else
            {
            //If IsOnDuty is false, set to true. If false, give error message
                return false;
            }
            
        } //end of Edit

        public bool ClockOut(int id)
        {
            CrewMember? existing = _repo.GetById(id);
            if (existing.IsOnDuty == true)
            {
                existing.IsOnDuty = false;
                _repo.Update(existing);
                return true;
            }
            else
            {
            //If IsOnDuty is false, set to true. If false, give error message
                return false;
            }
            
        } //end of Edit

        public List<CrewMemberReadDto> GetByRole(string role)
        {
            // IEnumerable<CrewMemberReadDto> result = _repo;

            // result = result.Where(c => c.Role == role);

            // return result.ToList();


            return _repo.GetByRole(role)
            .OrderBy(c => c.Role)
            .Select(c => ToReadDto(c))
            .ToList(); 
        }

        public bool AssignShipId(int id, int shipid)
        {
            CrewMember? existing = _repo.GetById(id);

            if(existing == null)
            {
                return false;
            }

            //we have to check if ship exists
            bool shipExists = _repoShip.GetAll().Any(s => s.Id == shipid);
            if(shipExists == false)
            {
                return false;
            }

            existing.ShipId = shipid;
            _repo.Update(existing);
            return true;
        }
                    // existing.ShipId = shipid.ShipId;  
    } //end of class
}//end of namespace