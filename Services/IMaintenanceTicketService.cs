using Starbase.StationOps.DTO;
using Starbase.StationOps.Dtos;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Services;

public interface IMaintenanceTicketService
{
    List<MaintenanceTicketReadDto> GetAll();
    MaintenanceTicketReadDto? GetById(int id);
    List<MaintenanceTicketReadDto> GetByShipId(int shipId);
    MaintenanceTicketReadDto? Create(MaintenanceTicketCreateDto ticket);            
    bool Update(int id, MaintenanceTicketCreateDto changes);
    MaintenanceTicketReadDto? Resolve(int id, MaintenanceTicketCreateDto changes);
    MaintenanceTicketReadDto? Reopen(int id, MaintenanceTicketCreateDto changes);
    public void Delete(int id);
    MaintenanceTicketReadDto? GetByShipId(int shipId); 
}
