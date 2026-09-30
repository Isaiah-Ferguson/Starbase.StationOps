using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps
{
    public interface IShipsRepository
    {
       List<Ship> GetAll(); 

       Ship? GetById(int id);

       Ship Add(Ship newShip);

       void Update(Ship newShip);

       void Delete(Ship newShip);
    }
}