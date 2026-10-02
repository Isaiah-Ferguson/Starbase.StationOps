using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps.DTOs
{
    public class MissionsReadDTO
    {
        public int Id {get;set;}
        public string Title {get;set;}=string.Empty;
        public int ShipId {get;set;}
        public string Status {get;set;}=string.Empty;
        
    }
}