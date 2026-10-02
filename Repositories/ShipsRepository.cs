using Starbase.StationOps.Data;

namespace Starbase.StationOps
{
    public class ShipsRepository : IShipsRepository
    {
        private readonly AppDbContext _db;

        public ShipsRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<Ship> GetAll()
        {
            return _db.Ships.ToList();
        }

        public Ship? GetById(int id)
        {
            return _db.Ships.FirstOrDefault(s => s.Id == id);
        }

        public Ship? GetByLowFuel(int fuelLevel)
        {
            return _db.Ships.FirstOrDefault(s => s.FuelPercent == fuelLevel);
        }



        public Ship Add(Ship ship)
        {
            _db.Ships.Add(ship);
            _db.SaveChanges();
            return ship;
        }

        public Ship Refuel(Ship ship)
        {
            
            _db.SaveChanges();

            return ship;
        }

        public void Update(Ship ship)
        {
            _db.SaveChanges();
        }

        public void Delete(Ship ship)
        {
            _db.Ships.Remove(ship);
            _db.SaveChanges();
        }
    }

}