using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps.Dtos
{
    public class DockingBayReadDto
    {
        public int Id {get; set;}
        public int BayNumber {get; set;}
        public int ShipId {get; set;}
    }
}