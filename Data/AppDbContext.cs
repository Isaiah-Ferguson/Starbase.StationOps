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
}
