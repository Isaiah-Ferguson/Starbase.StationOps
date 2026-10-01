
namespace Starbase.StationOps.Models
{
    public class Missions
    {
        public int Id {get;set;}
        public string Title {get;set;}=string.Empty;
        public int ShipId {get;set;}
        public string Status {get;set;}=string.Empty;
    }
}