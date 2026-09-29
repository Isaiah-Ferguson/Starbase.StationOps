using Microsoft.EntityFrameworkCore;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Data;

// The connection to the database. Each DbSet is one table.
// EVERYONE adds one line here, each in their own spot, so nobody's lines collide.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Example — already done
    public DbSet<Sector> Sectors { get; set; }

    // Student 1 — Crew (Callen)
    // TODO: public DbSet<CrewMember> CrewMembers { get; set; }

    // Student 2 — Ships (Zackory)
    // TODO: public DbSet<Ship> Ships { get; set; }

    // Student 3 — Missions (Chris)
    // TODO: public DbSet<Mission> Missions { get; set; }

    // Student 4 — Docking Bays (Valery)
    // TODO: public DbSet<DockingBay> DockingBays { get; set; }

    // Student 5 — Maintenance Tickets (Zionn)
    // TODO: public DbSet<MaintenanceTicket> MaintenanceTickets { get; set; }

    // Student 6 — Visitors (Brandon)
    // TODO: public DbSet<Visitor> Visitors { get; set; }
}
