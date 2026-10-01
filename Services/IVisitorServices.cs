using Starbase.StationOps.DTOs;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Services
{
    public interface IVisitorServices
    {
        List<VisitorReadDTO> GetAll();
    VisitorReadDTO? GetById(int id);
    VisitorReadDTO? Create(VisitorCreateDTO newVisitor);             
    bool Update(int id , Visitors changes);
    void Delete(int id );
    }
}