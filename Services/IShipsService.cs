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

      ShipCreateDTO Create(ShipCreateDTO newShip);

      bool Update(Ship existing, Ship changes);

      void Delete(int id);
    }
}