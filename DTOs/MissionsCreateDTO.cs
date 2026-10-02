using System;
using System.ComponentModel.DataAnnotations;

namespace Starbase.StationOps.DTOs
{
    public class MissionsCreateDTO
    {
         [Required(ErrorMessage = "Must input a name")]
        [StringLength(60, ErrorMessage = "Name can not be greater than 40 characters")]
        public string Title {get;set;}=string.Empty;
        public int ShipId {get;set;}
        [AllowedValues("Planned", "Active", "Complete", 
        ErrorMessage =  "A Status can only be 'Planned', 'Active', or 'Complete'")]
        public string Status {get;set;}=string.Empty;
    }
}