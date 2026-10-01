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
            return _repository.GetAll().OrderBy(v => v.Name).Select(v => ToReadDTO(v)).ToList();
        }

        public VisitorReadDTO? GetById(int id)
        {
            Visitors? visitor = _repository.GetById(id);

            if (visitor == null)
            {
                return null;
            }

            return ToReadDTO(visitor);
        }

        public VisitorReadDTO? Create(VisitorCreateDTO dto)
        {

            bool exsists = _repository.GetAll().Any(v => v.Name.ToLower() == dto.Name.ToLower());


            if (exsists)
            {
                return null;
            }


            Visitors visitor = new Visitors();

            visitor.Name = dto.Name;
            visitor.HomePlanet = dto.HomePlanet;
            visitor.IsCleared = dto.IsCleared;
            visitor.ShipId = dto.ShipId;
            visitor.StorageLocation = "Visitors List";


            Visitors created = _repository.Add(visitor);

            return ToReadDTO(created);



        }

        public bool Update(int id, Visitors changes)
        {
            if (!IsValid(changes))
            {
                return false;
            }

            Visitors? exsisting = _repository.GetById(id);

            exsisting.Name = changes.Name;
            exsisting.HomePlanet = changes.HomePlanet;
            exsisting.IsCleared = changes.IsCleared;
            exsisting.ShipId = changes.ShipId;

            _repository.Update(exsisting);

            return true;
        }

        public void Delete(int id)
        {
            Visitors? exsisting = _repository.GetById(id);

         if (exsisting != null)
            {
                 _repository.Delete(exsisting);
            }


           
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