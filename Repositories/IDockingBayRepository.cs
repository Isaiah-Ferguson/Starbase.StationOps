using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories
{
    public interface IDockingBayRepository
    {
        List<DockingBay> GetAll();
        DockingBay? GetById(int id);
        DockingBay Add (DockingBay dockingBay);
        void Update (int id, DockingBay dockingBay);
        void Delete (DockingBay dockingBay);
        bool IsBayAvailable(int id);
    }
}