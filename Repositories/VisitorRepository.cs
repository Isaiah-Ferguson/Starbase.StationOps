using Starbase.StationOps.Data;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly AppDbContext _db;

        public VisitorRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<Visitors> GetAll()
        {
            return _db.Visitors.ToList();
        }

        public Visitors? GetById(int id)
        {
            return _db.Visitors.FirstOrDefault(v => v.Id == id);
        }

        public Visitors Add(Visitors newVisitor)
        {
            _db.Visitors.Add(newVisitor);
            _db.SaveChanges();
            return newVisitor;
        }

        public void Update(Visitors exsistingVisitor)
        {
            _db.SaveChanges();
        }

        public void Delete(Visitors exsistingVisitor)
        {
            _db.Visitors.Remove(exsistingVisitor);
            _db.SaveChanges();
        }
    }
}