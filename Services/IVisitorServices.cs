using Starbase.StationOps.DTOs;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Services
{
    public interface IVisitorServices
    {
        List<VisitorReadDTO> GetAll();
    VisitorReadDTO? GetById(int id);
    List<VisitorReadDTO> GetByPlanet(string homePlanet);
    VisitorReadDTO? Create(VisitorCreateDTO newVisitor);             
    bool Update(int id , Visitors changes);
    bool IsCleared(int id);
    void Delete(int id );
    }
}