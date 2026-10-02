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

       Ship? GetByLowFuel(int fuelLevel);

       Ship Add(Ship ship);

       Ship Refuel(Ship refueled);

       void Update(Ship ship);

       void Delete(Ship ship);
    }
}