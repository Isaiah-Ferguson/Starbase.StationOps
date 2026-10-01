using System;
using System.ComponentModel.DataAnnotations;

namespace Starbase.StationOps.DTOs
{
    public class MissionsCreateDTO
    {
        [Required, StringLength(60) ]
        public string Title {get;set;}=string.Empty;
        public int ShipId {get;set;}
        [AllowedValues("Planned", "Active", "Complete")]
        public string Status {get;set;}=string.Empty;
    }
}