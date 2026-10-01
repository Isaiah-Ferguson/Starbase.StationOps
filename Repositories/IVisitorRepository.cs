using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories
{
    public interface IVisitorRepository
    {
        List<Visitors> GetAll();
    Visitors? GetById(int id);
    Visitors? Add(Visitors newVisitor);             
    void Update(Visitors existingVisitor);
    void Delete(Visitors visitor);
    }
}