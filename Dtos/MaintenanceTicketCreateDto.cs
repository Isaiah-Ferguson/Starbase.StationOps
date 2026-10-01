using System.ComponentModel.DataAnnotations;

namespace Starbase.StationOps.DTO
{
    public class MaintenanceTicketCreateDto
    {
        //Use for HttpPost and HttpPut'
        public int ShipId { get; set; }

        [Required(ErrorMessage = "Every ship needs a description and is not more than 200 letters long.")]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
    }
}