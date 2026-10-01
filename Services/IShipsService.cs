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
      ShipReadDTO Create(ShipCreateDTO ship);
      bool Update(int id, Ship changes);
      void Delete(int id);
    }
}