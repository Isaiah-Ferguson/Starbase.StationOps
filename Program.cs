using Microsoft.EntityFrameworkCore;
using Starbase.StationOps;
using Starbase.StationOps.Data;
using Starbase.StationOps.Repositories;
using Starbase.StationOps.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<ISectorService, SectorService>();
builder.Services.AddScoped<IMissionsRepository, MissionsRepository>();
builder.Services.AddScoped<IMissionsService, MissionsService>();

builder.Services.AddScoped<ICrewMemberRepository, CrewMemberRepository>();
builder.Services.AddScoped<ICrewMemberService, CrewMemberService>(); 

builder.Services.AddScoped<IDockingBayService, DockingBayService>();
builder.Services.AddScoped<IDockingBayRepository, DockingBayRepository>();
builder.Services.AddScoped<IShipsService, ShipsService>();
builder.Services.AddScoped<IShipsRepository, ShipsRepository>();

builder.Services.AddScoped<IMaintenanceTicketService, MaintenanceTicketService>();
builder.Services.AddScoped<IMaintenanceTicketRepository, MaintenanceTicketRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapControllers();

app.Run();
