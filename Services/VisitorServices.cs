using Starbase.StationOps.Models;
using Starbase.StationOps.Repositories;

namespace Starbase.StationOps.Services
{
    public class VisitorServices : IVisitorServices
    {

        private readonly IVisitorRepository _repository;
        public VisitorServices(IVisitorRepository repository )
        {
            _repository = repository;
        }

        public List<Visitors> GetAll()
        {
            return _repository.GetAll().OrderBy(v => v.Name).ToList();
        }

        public Visitors? GetById(int id)
        {
            return _repository.GetById(id);
        }
    }
}