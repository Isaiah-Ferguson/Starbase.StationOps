using Starbase.StationOps.Models;

namespace Starbase.StationOps.Services;

public interface ISectorService
{
    List<Sector> GetAll();
    Sector? GetById(int id);
    Sector? Create(Sector sector);             
    bool Update(Sector existing, Sector changes);
    void Delete(Sector sector);
}
