using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps
{
    public class ShipReadDTO
    {
        public int Id {get; set; }

        public string Name {get; set; } 

        public string Captain {get; set; }

        public int FuelPercent {get; set; }
    }
}