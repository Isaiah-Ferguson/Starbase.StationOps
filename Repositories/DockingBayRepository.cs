using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Starbase.StationOps.Data;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories
{
    public class DockingBayRepository : IDockingBayRepository
    {
        private readonly AppDbContext _db;

        public DockingBayRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<DockingBay> GetAll()
        {
            return _db.DockingBay.ToList();
        }


        public bool IsBayAvailable(int id)
        {
            return _db.DockingBay.Any(d => d.ShipId == id && d.ShipId == 0);
        }

        public DockingBay? GetById(int id)
        {
            return _db.DockingBay.FirstOrDefault(d => d.Id == id);
        }

        public DockingBay Add(DockingBay dockingBay)
        {
            _db.DockingBay.Add(dockingBay);
            _db.SaveChanges();
            return dockingBay;
        }

        public void Update (int id, DockingBay dockingBay)
        {
            _db.SaveChanges();
        }

        public void Delete (DockingBay dockingBay)
        {
            _db.DockingBay.Remove(dockingBay);
            _db.SaveChanges();
        }

        public bool DockShip(int id)
        {
            _db.SaveChanges();
            return true;
        }

        public bool UndockShip(int id)
        {
            _db.SaveChanges();
            return true;
        }
    }
}