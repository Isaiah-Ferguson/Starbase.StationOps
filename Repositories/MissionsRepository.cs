
using Starbase.StationOps.Data;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories
{
    public class MissionsRepository : IMissionsRepository
    {
        private readonly AppDbContext _db;

        public MissionsRepository(AppDbContext db)
    {
        _db = db;
    }

    public List<Missions> GetAll()
    {
        return _db.Missions.ToList();
    }

    public Missions? GetById(int id)
    {
        return _db.Missions.FirstOrDefault(s => s.Id == id);
    }

    public Missions Add(Missions missions)
    {
        _db.Missions.Add(missions);
        _db.SaveChanges();
        return missions;
    }

    public void Update(Missions missions)
    {
        _db.SaveChanges();
    }

    public void Delete(Missions missions)
    {
        _db.Missions.Remove(missions);
        _db.SaveChanges();
    }
    }
}