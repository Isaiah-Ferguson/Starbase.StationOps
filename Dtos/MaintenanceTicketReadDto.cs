using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps.Dtos
{
    //Use for HttpGet
    public class MaintenanceTicketReadDto
    {
        public int Id { get; set; }
        public int ShipId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsResolved { get; set; }  
           
    }
}