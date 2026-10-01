namespace Starbase.StationOps.Models
{
    public class MaintenanceTicket
    {
        public int Id { get; set; }
        public int ShipId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsResolved { get; set; } 
    }
}