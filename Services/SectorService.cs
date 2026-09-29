using Starbase.StationOps.Models;
using Starbase.StationOps.Repositories;

namespace Starbase.StationOps.Services;

// EXAMPLE — the rules for sectors. It never touches the database; it asks the repository.
public class SectorService : ISectorService
{
    private readonly ISectorRepository _repository;

    public SectorService(ISectorRepository repository)
    {
        _repository = repository;
    }

    public List<Sector> GetAll()
    {
        return _repository.GetAll().OrderBy(s => s.Name).ToList();
    }

    public Sector? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public Sector? Create(Sector sector)
    {
        if (!IsValid(sector))
        {
            return null;
        }

        return _repository.Add(sector);
    }

    public bool Update(Sector existing, Sector changes)
    {
        if (!IsValid(changes))
        {
            return false;
        }

        existing.Name = changes.Name;
        existing.SecurityLevel = changes.SecurityLevel;

        _repository.Update(existing);
        return true;
    }

    public void Delete(Sector sector)
    {
        _repository.Delete(sector);
    }

    // The rules, in one place: a sector needs a name, and a security level from 1 to 5.
    private bool IsValid(Sector sector)
    {
        if (string.IsNullOrWhiteSpace(sector.Name))
        {
            return false;
        }

        if (sector.SecurityLevel < 1 || sector.SecurityLevel > 5)
        {
            return false;
        }

        return true;
    }
}
