using Microsoft.EntityFrameworkCore;
using Starbase.StationOps.Data;
using Starbase.StationOps.Repositories;
using Starbase.StationOps.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Use SQLite. The whole database is one file, named in appsettings.json.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// EVERYONE registers their repository and service here, each in their own spot.

// Example — already done
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<ISectorService, SectorService>();

// Student 1 — Crew (Callen)
// TODO: repository line (Day 1) and service line (Day 2)

// Student 2 — Ships (Zackory)
// TODO: repository line (Day 1) and service line (Day 2)

// Student 3 — Missions (Chris)
// TODO: repository line (Day 1) and service line (Day 2)

// Student 4 — Docking Bays (Valery)
// TODO: repository line (Day 1) and service line (Day 2)

// Student 5 — Maintenance Tickets (Zionn)
// TODO: repository line (Day 1) and service line (Day 2)

// Student 6 — Visitors (Brandon)
// TODO: repository line (Day 1) and service line (Day 2)

var app = builder.Build();

app.MapControllers();

app.Run();
