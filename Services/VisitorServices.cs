using Starbase.StationOps.DTOs;
using Starbase.StationOps.Models;
using Starbase.StationOps.Repositories;

namespace Starbase.StationOps.Services
{
    public class VisitorServices : IVisitorServices
    {

        private readonly IVisitorRepository _repository;
        public VisitorServices(IVisitorRepository repository)
        {
            _repository = repository;
        }

        public List<VisitorReadDTO> GetAll()
        {
            return _repository.GetAll().OrderBy(v => v.Name).ToList();
        }

        public VisitorReadDTO? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Visitors? Create(Visitors newVisitor)
        {




            if (!IsValid(newVisitor))
            {
                return null;
            }
            newVisitor.IsCleared = false;

            return _repository.Add(newVisitor);
        }

        public bool Update(Visitors exsisting, Visitors changes)
        {
            if (!IsValid(changes))
            {
                return false;
            }

            exsisting.Name = changes.Name;
            exsisting.HomePlanet = changes.HomePlanet;
            exsisting.IsCleared = changes.IsCleared;
            exsisting.ShipId = changes.ShipId;

            _repository.Update(exsisting);

            return true;
        }

        public void Delete(Visitors visitor)
        {
            _repository.Delete(visitor);
        }


        private bool IsValid(Visitors visitor)
        {
            if (string.IsNullOrWhiteSpace(visitor.Name))
            {
                return false;
            }

            if (visitor.IsCleared == false)
            {
                return false;
            }

            return true;
        }

        private static VisitorReadDTO ToReadDTO(Visitors visitors)
        {
            VisitorReadDTO outputDTO = new VisitorReadDTO();

           outputDTO.Id = visitors.Id;
            outputDTO.Name = visitors.Name;
           outputDTO.HomePlanet = visitors.HomePlanet;
            outputDTO.IsCleared = visitors.IsCleared;
            outputDTO.ShipId = visitors.ShipId;

            return outputDTO;
        }
    }
}