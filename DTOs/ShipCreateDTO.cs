using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps
{
    public class ShipCreateDTO
    {
        [Required(ErrorMessage = "Please Enter a Username.")]
        [StringLength(40)]
        public string Name {get; set; }

        [Required(ErrorMessage ="Please Enter a Captain Name")]
        [StringLength(40)]
        public string Captain {get; set; } 

        [Range(0, 100)]
        public int FuelPercent {get; set;}
    }
}