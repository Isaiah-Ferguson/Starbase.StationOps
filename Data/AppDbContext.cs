using Microsoft.EntityFrameworkCore;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Sector> Sectors { get; set; }
    public DbSet<DockingBay> DockingBay {get; set;}
    public DbSet<MaintenanceTicket> Maintain { get; set; }

    public DbSet<Ship> Ships { get; set; }
    public DbSet<CrewMember> CrewMembers {get; set;}
    public DbSet<Missions> Missions {get;set;}
}
