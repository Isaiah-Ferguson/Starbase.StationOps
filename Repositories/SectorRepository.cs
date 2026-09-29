using Starbase.StationOps.Data;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories;

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
        _db.Sectors.Add(sector);
        _db.SaveChanges();
        return sector;
    }

    public void Update(Sector sector)
    {
        _db.SaveChanges();
    }

    public void Delete(Sector sector)
    {
        _db.Sectors.Remove(sector);
        _db.SaveChanges();
    }
}
