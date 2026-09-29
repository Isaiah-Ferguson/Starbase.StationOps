namespace Starbase.StationOps.Models;

// EXAMPLE RESOURCE — already done. Copy this pattern for your own model.
// One sector of the station, and how restricted it is (1 = open, 5 = top secret).
public class Sector
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SecurityLevel { get; set; }
}
