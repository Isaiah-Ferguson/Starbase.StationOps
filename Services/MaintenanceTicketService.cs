using Microsoft.AspNetCore.Http.HttpResults;
using Starbase.StationOps.DTO;
using Starbase.StationOps.Dtos;
using Starbase.StationOps.Models;
using Starbase.StationOps.Repositories;

namespace Starbase.StationOps.Services;

public class MaintenanceTicketService : IMaintenanceTicketService
{
    private readonly IMaintenanceTicketRepository _repository;
    private readonly IShipsRepository _ships;

    public MaintenanceTicketService(IMaintenanceTicketRepository repository, IShipsRepository ships)
    {
        _repository = repository;
        _ships = ships;//added repository and constructor to pull from to verify ship information
    }
    public List<MaintenanceTicketReadDto> GetAll()
    {
        return _repository.GetAll()
        .OrderBy(m => m.ShipId)
        .Select(m => ToReadDTO(m))
        .ToList();
    }

    public List<MaintenanceTicketReadDto> GetListByShipId(int shipId)
    {
        return _repository.GetAll().Where(ticket => ticket.ShipId == shipId)
            .OrderBy(ticket => ticket.Id)
            .Select(ToReadDTO)
            .ToList();
    }

    public MaintenanceTicketReadDto? GetById(int id)
    {
        MaintenanceTicket? ticket = _repository.GetById(id);

        if (ticket is null)
        {
            return null;
        }
        return ToReadDTO(ticket);
        }
       public MaintenanceTicketReadDto? GetByShipId(int shipId)
    {
         MaintenanceTicket? ticket = _repository.GetByShipId(shipId);

            if (ticket is null)
            {
                return null;
            }

            return ToReadDTO(ticket);
    }

    public MaintenanceTicketReadDto? Create(MaintenanceTicketCreateDto ticket)
    {
        if (!IsValidCreateDto(ticket))
        {
            return null;
        }

        MaintenanceTicket Maintain = new MaintenanceTicket();

        Maintain.ShipId = ticket.ShipId;
        Maintain.Description = ticket.Description;
        Maintain.IsResolved = false; //everything new starts here

        //We are creating a new Supply variable and storing our added supply.
        MaintenanceTicket created = _repository.Add(Maintain);

        return ToReadDTO(created);
    }

    public bool Update(int id, MaintenanceTicketCreateDto changes)
    {
        MaintenanceTicket? existing = _repository.GetById(id);

        if (existing is null || !IsValidCreateDto(changes))
        {
            return false;
        }

        existing.ShipId = changes.ShipId;
        existing.Description = changes.Description;


        _repository.Update(existing);
        return true;
    }

    public MaintenanceTicketReadDto? Resolve(int id, MaintenanceTicketCreateDto changes)
    {
        MaintenanceTicket? existing = _repository.GetById(id);

        if (existing is null || existing.IsResolved)
        {
            return null;
        }

        existing.IsResolved = true;
        existing.ShipId = changes.ShipId;
        existing.Description = changes.Description;
        _repository.Update(existing);

        return ToReadDTO(existing);
    }

    public MaintenanceTicketReadDto? Reopen(int id, MaintenanceTicketCreateDto changes)
    {
        MaintenanceTicket? existing = _repository.GetById(id);

        if (existing is null || !existing.IsResolved)
        {
            return null;
        }

        existing.IsResolved = false;
        existing.ShipId = changes.ShipId;
        existing.Description = changes.Description;
        _repository.Update(existing);

        return ToReadDTO(existing);
    }

    public void Delete(int id)
    {
        MaintenanceTicket? existing = _repository.GetById(id);

        if (existing != null)
        {
            _repository.Delete(existing);
        }
    }

    private bool IsValidCreateDto(MaintenanceTicketCreateDto ticket)
    {

        if (string.IsNullOrWhiteSpace(ticket.Description) || ticket.ShipId <= 0)
        {
            return false;
        }

        return _ships.GetById(ticket.ShipId) is not null;
    }



    private static MaintenanceTicketReadDto ToReadDTO(MaintenanceTicket ticket)
    {
        MaintenanceTicketReadDto outputDTO = new MaintenanceTicketReadDto();
        // Random rnd = new Random();
        outputDTO.Id = ticket.Id;
        outputDTO.ShipId = ticket.ShipId;
        outputDTO.Description = ticket.Description;
        outputDTO.IsResolved = ticket.IsResolved;

        return outputDTO;
    }

    private static MaintenanceTicketCreateDto ToCreateDTO(MaintenanceTicket ticket)
    {
        MaintenanceTicketCreateDto outputDTO = new MaintenanceTicketCreateDto();
        // Random rnd = new Random();

        outputDTO.ShipId = ticket.ShipId;
        outputDTO.Description = ticket.Description;

        return outputDTO;
    }
}
