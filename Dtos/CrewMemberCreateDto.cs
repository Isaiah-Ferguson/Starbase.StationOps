using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps.Dtos
{
    public class CrewMemberCreateDto
    {
        [Required(ErrorMessage = "Must input a name")]
        [StringLength(40, ErrorMessage = "Name can not be greater than 40 characters")]
        public string Name{get; set;} = string.Empty; 
        [Required(ErrorMessage = "Must input a role")]
        [StringLength(30, ErrorMessage = "Role cannot be greater than 30 characters")]
        public string Role{get; set;} = string.Empty;
        public bool IsOnDuty{get; set;}
    }
}