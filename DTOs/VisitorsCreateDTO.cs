using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps.DTOs
{
    public class VisitorCreateDTO
    {
        [Required (ErrorMessage = "Visitors names and home planet can only have a maximum of 40 characters")]
        public string Name {get;set;}
        [StringLength(maximumLength:40)]
        public string HomePlanet {get;set;}

        public int ShipId {get;set;}

        public bool IsCleared {get;set;}
    }
}