using Starbase.StationOps.Data;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories;

public class MaintenanceTicketRepository : IMaintenanceTicketRepository

{
    private readonly AppDbContext _db;

    public MaintenanceTicketRepository(AppDbContext db)
    {
        _db = db;
    }

    public List<MaintenanceTicket> GetAll()
    {
        return _db.Maintain.ToList();
    }

    public MaintenanceTicket? GetById(int id)
    {
        return _db.Maintain.FirstOrDefault(m => m.Id == id);
    }
    public MaintenanceTicket? GetByShipId(int shipId)
    {
        return _db.Maintain.FirstOrDefault(m => m.ShipId == shipId); 
    }

   public List<MaintenanceTicket> GetListByShipId(int shipId)
{
    return _db.Maintain
        .Where(ticket => ticket.ShipId == shipId)
        .ToList();
}

    public MaintenanceTicket Add(MaintenanceTicket ticket)
    {
        _db.Maintain.Add(ticket);
        _db.SaveChanges();
        return ticket;
    }

    public void Update(MaintenanceTicket ticket)
    {
        _db.SaveChanges();
    }


    public void Delete(MaintenanceTicket ticket)
    {
        _db.Maintain.Remove(ticket);
        _db.SaveChanges();
    }
}

