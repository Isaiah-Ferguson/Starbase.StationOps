using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps.Dtos
{
    public class CrewMemberReadDto
    {
         public int Id{get; set; }
        public string Name{get; set;} = string.Empty; 
        public string Role{get; set;} = string.Empty;
        public bool IsOnDuty{get; set;}
         public int ShipId{get; set;}
    }
}