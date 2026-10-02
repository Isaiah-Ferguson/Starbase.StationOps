using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories
{
    public interface IMaintenanceTicketRepository
    {
        List<MaintenanceTicket> GetAll();
        MaintenanceTicket? GetById(int id);
        MaintenanceTicket Add(MaintenanceTicket ticket);
        List<MaintenanceTicket> GetListByShipId(int shipId);
        void Update(MaintenanceTicket ticket);
        void Delete(MaintenanceTicket ticket);
        MaintenanceTicket? GetByShipId(int shipId); 
    }
}