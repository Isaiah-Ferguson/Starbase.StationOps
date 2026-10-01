using Starbase.StationOps.DTO;
using Starbase.StationOps.Dtos;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Services;

public interface IMaintenanceTicketService
{
    List<MaintenanceTicketReadDto> GetAll();
    MaintenanceTicketReadDto? GetById(int id);
    MaintenanceTicketReadDto? Create(MaintenanceTicketCreateDto ticket);            
    bool Update(int id, MaintenanceTicketCreateDto changes);
    public void Delete(int id);
}
