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

builder.Services.AddScoped<IShipsService, ShipsService>();

var app = builder.Build();

app.MapControllers();

app.Run();
