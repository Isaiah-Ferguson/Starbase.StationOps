using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Starbase.StationOps.Data;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories
{
    public interface ICrewMemberRepository
    {
       List<CrewMember> GetAll(); 
      CrewMember? GetById(int id); 
      CrewMember Add(CrewMember newCrewMember);
      void Update(CrewMember newCrewMember);
      void Delete(CrewMember deleteCrewMember); 

    }
}