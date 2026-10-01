using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories
{
    public interface IMissionsRepository
    {
        List<Missions> GetAll();
        Missions? GetById(int id);
        Missions Add(Missions missions);
        void Update(Missions missions);
        void Delete (Missions missions);
    }
}