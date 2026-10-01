using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps.DTOs
{
    public class VisitorReadDTO
    {
        public int Id {get;set;}

        public string Name {get;set;} = string.Empty;

        public string HomePlanet {get;set;}

        public int ShipId {get;set;}

        public bool IsCleared {get;set;}
    }
}