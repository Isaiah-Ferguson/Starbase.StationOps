using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories;

// EXAMPLE — the repository promises. Storage only, no rules.
// Every resource's repository has these same five methods.
public interface ISectorRepository
{
    List<Sector> GetAll();
    Sector? GetById(int id);
    Sector Add(Sector sector);
    void Update(Sector sector);
    void Delete(Sector sector);
}
