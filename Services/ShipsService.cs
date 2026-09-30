using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starbase.StationOps
{
    public class ShipsService : IShipsService
    {
        private readonly IShipsRepository _shipsrepository;

        public ShipsService (IShipsRepository shipsrepository)
        {
            _shipsrepository = shipsrepository;
        }

        //Get All
        public List<ShipReadDTO> GetAll()
        {
            return _shipsrepository.GetAll().OrderBy(s => s.Name).Select(s => ToReadDTO(s)).ToList();
        }

        //Get By ID

        public ShipReadDTO? GetById(int id)
        {
            Ship? ship = _shipsrepository.GetById(id);

            if (ship == null)
            {
                return null;
            }

            return ToReadDTO(ship);
        }

        //Create 
        public ShipReadDTO? Create(ShipCreateDTO dto)
        {
            bool exists = _shipsrepository.GetAll().Any(s => s.Name.ToLower() == dto.Name.ToLower());

            if (exists)
            {
                return null;
            }

            Ship ship = new Ship();

            ship.Name = dto.Name;
            ship.Captain = dto.Captain;
            ship.FuelPercent = dto.FuelPercent;

            Ship created = _shipsrepository.Add(ship);

            return ToReadDTO(created);
        }

        //Update
        public bool Update(Ship existing, Ship changes)
        {
        
        }

        //Delete
        public void Delete(int id)
        {
            Ship? ship = _shipsrepository.GetById(id);

            if(ship != null)
            {
                _shipsrepository.Delete(ship);
            }
        }

        //Reader
        public static ShipReadDTO ToReadDTO(Ship newShip)
        {
            ShipReadDTO outputDTO = new ShipReadDTO();
            outputDTO.Id = newShip.Id;
            outputDTO.Name = newShip.Name;
            outputDTO.Captain = newShip.Captain;
            outputDTO.FuelPercent = newShip.FuelPercent;

            return outputDTO;
        }

        //Validation


    }
}