using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories;

public interface ISectorRepository
{
    List<Sector> GetAll();
    Sector? GetById(int id);
    Sector Add(Sector sector);
    void Update(Sector sector);
    void Delete(Sector sector);
}
