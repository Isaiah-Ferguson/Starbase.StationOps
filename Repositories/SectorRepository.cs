using Starbase.StationOps.Data;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories;

// EXAMPLE — the only class that touches the Sectors table.
public class SectorRepository : ISectorRepository
{
    private readonly AppDbContext _db;

    public SectorRepository(AppDbContext db)
    {
        _db = db;
    }

    public List<Sector> GetAll()
    {
        return _db.Sectors.ToList();
    }

    public Sector? GetById(int id)
    {
        return _db.Sectors.FirstOrDefault(s => s.Id == id);
    }

    public Sector Add(Sector sector)
    {
        sector.Id = 0;                  // the database picks the id

        _db.Sectors.Add(sector);
        _db.SaveChanges();
        return sector;
    }

    public void Update(Sector sector)
    {
        // The sector came out of the database through GetById, so EF Core
        // is already watching it. Saving writes the changes.
        _db.SaveChanges();
    }

    public void Delete(Sector sector)
    {
        _db.Sectors.Remove(sector);
        _db.SaveChanges();
    }
}
