namespace Starbase.StationOps.Models
{
    public class Visitors
    {
        public int Id {get;set;}

        public string Name {get;set;} = string.Empty;

        public string HomePlanet {get;set;} = string.Empty;

        public int ShipId {get;set;}

        public bool IsCleared {get;set;}

        public string StorageLocation {get;set;} = string.Empty;
    }
}