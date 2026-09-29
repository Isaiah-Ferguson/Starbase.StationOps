using Starbase.StationOps.Models;

namespace Starbase.StationOps.Services;

// EXAMPLE — the service promises. This is where the rules live.
public interface ISectorService
{
    List<Sector> GetAll();
    Sector? GetById(int id);
    Sector? Create(Sector sector);                  // null if it breaks a rule
    bool Update(Sector existing, Sector changes);   // false if the changes break a rule
    void Delete(Sector sector);
}
