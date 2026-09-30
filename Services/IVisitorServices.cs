using Starbase.StationOps.Models;

namespace Starbase.StationOps.Services
{
    public interface IVisitorServices
    {
        List<Visitors> GetAll();
    Visitors? GetById(int id);
    Visitors? Create(Visitors newVisitor);             
    bool Update(Visitors existing, Visitors changes);
    void Delete(Visitors visitor);
    }
}