using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Starbase.StationOps.Dtos;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Services
{
    public interface IDockingBayService
    {
        List<DockingBayReadDto> GetAll();
        DockingBayReadDto? GetById(int id);
        DockingBayReadDto Add(DockingBayCreateDto dockingBay);
        bool Update(int id, DockingBay dockingBay);
        void Delete (int id); 

        bool IsBayAvailable(int id);
    }
}