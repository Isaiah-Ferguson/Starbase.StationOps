using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Starbase.StationOps.Models;
using Starbase.StationOps.Repositories;

namespace Starbase.StationOps
{
    public class ShipsService : IShipsService
    {
        private readonly IShipsRepository _shipsrepository;

        private readonly IMaintenanceTicketRepository _ticketRepository;

        

        public ShipsService(IShipsRepository shipsrepository, IMaintenanceTicketRepository ticketRepository)
        {
            _shipsrepository = shipsrepository;

            _ticketRepository = ticketRepository;
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
        public bool Update(int id, Ship changes)
        {
            Ship? ship = _shipsrepository.GetById(id);

            if (ship == null)
            {
                return false;
            }

            ship.Name = changes.Name;
            ship.Captain = changes.Captain;
            ship.FuelPercent = changes.FuelPercent;

            _shipsrepository.Update(ship);
            return true;
        }

        //Delete
        
            //Block deleting a ship with open tickets (Using a teammate's repository)
            
           
            //The service asks for IMaintenanceTicketRepository from Zionn (#5 Maintenance Tickets).
        public void Delete(int id)
        {
            Ship? ship = _shipsrepository.GetById(id);

             // if the ship has any maintenance ticket that isn't resolved.
            //DELETE /api/ships/{id} returns 400 while

            List <MaintenanceTicket>? shipTickets = _ticketRepository.GetAll().Where(t => t.ShipId == id && t.IsResolved == false).ToList();

            //if the ship is not null & the ticket is are resolved then we can delete
            if (ship != null && shipTickets.Count == 0)
            {
                _shipsrepository.Delete(ship);
            }

        }


        //GetByLowFuel
        public List<ShipReadDTO>? GetByLowFuel(int threshhold)
        {
            return _shipsrepository.GetAll().Where(s => s.FuelPercent < threshhold).OrderBy(s => s.FuelPercent).Select(s => ToReadDTO(s)).ToList();
        }

        public bool Refuel(int id)
        {
            Ship? ship = _shipsrepository.GetById(id);

            if (ship == null)
            {
                return false;
            }

            ship.FuelPercent = 100;

            _shipsrepository.Update(ship);

            return true;
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

    }
}