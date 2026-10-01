using Starbase.StationOps.DTOs;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Services
{
    public interface IVisitorServices
    {
        List<VisitorReadDTO> GetAll();
    VisitorReadDTO? GetById(int id);
    Visitors? Create(Visitors newVisitor);             
    bool Update(Visitors existing, Visitors changes);
    void Delete(Visitors visitor);
    }
}