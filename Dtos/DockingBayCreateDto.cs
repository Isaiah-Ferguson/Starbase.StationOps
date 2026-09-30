using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps.Dtos
{
    public class DockingBayCreateDto
    {
        [Range(1, 20, ErrorMessage = "Bay Number must be from 1 to 20.")]
        public int BayNumber {get; set;}
    }
}