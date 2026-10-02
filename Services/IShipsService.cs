using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps
{
    public interface IShipsService
    {
      List<ShipReadDTO> GetAll();  
      ShipReadDTO? GetById(int id);

      List<ShipReadDTO>? GetByLowFuel(int shipId);
      ShipReadDTO Create(ShipCreateDTO ship);

      bool Refuel(int id);
      bool Update(int id, Ship changes);
      void Delete(int id);
    }
}