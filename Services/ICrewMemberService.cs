using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Starbase.StationOps.Dtos;

namespace Starbase.StationOps.Services
{
    public interface ICrewMemberService
    {
        List<CrewMemberReadDto> GetAll(); 
        CrewMemberReadDto? GetById(int id); 
        CrewMemberReadDto? Create(CrewMemberCreateDto newCrewMember);
        bool Edit(int id, CrewMemberReadDto editMember);
        void Delete(int id); 
        
    }
}