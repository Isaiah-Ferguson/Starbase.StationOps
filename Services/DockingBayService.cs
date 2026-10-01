using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Starbase.StationOps.Dtos;
using Starbase.StationOps.Migrations;
using Starbase.StationOps.Models;
using Starbase.StationOps.Repositories;

namespace Starbase.StationOps.Services
{
    public class DockingBayService : IDockingBayService
    {
        private readonly IDockingBayRepository _repository;
        public DockingBayService(IDockingBayRepository repository)
        {
            _repository = repository;
        }

        public List<DockingBayReadDto> GetAll()
        {
            return _repository.GetAll()
            .Select(d => ToReadDto(d)) //turns every model into DTO
            .ToList();
        }

        public DockingBayReadDto? GetById(int id)
        {
            DockingBay? dockingBay = _repository.GetById(id);
            if (dockingBay == null)
            {
                return null;
            }
            return ToReadDto(dockingBay);
        }

        //Rule: No 2 bays can share the same number
        public DockingBayReadDto? Add(DockingBayCreateDto dto)
        {
            bool existingBay = _repository.GetAll().Any(d => d.BayNumber == dto.BayNumber);
            if (existingBay)
            {
                return null;
            }

            //DTO to Model
            DockingBay dockingBay = new DockingBay();

            dockingBay.BayNumber = dto.BayNumber;

            DockingBay created = _repository.Add(dockingBay);

            return ToReadDto(created);

        }

        //Rule: No 2 bays can share the same number, when updating don't count the bay you're upating
        public bool Update(int id, DockingBay dockingBay)
        {
            //dockingBay param is being sent to the api
            //the exists is the actual record from the db 
            bool existingBay = _repository.GetAll().Any(d => d.BayNumber == dockingBay.BayNumber);

            DockingBay? exists = _repository.GetById(id);
            if (exists == null || existingBay)
            {
                return false;
            }

            exists.BayNumber = dockingBay.BayNumber;

            _repository.Update(id, exists);
            return true;
        }

        public void Delete(int id)
        {
            DockingBay? dockingBay = _repository.GetById(id);

            if (dockingBay != null)
            {
                _repository.Delete(dockingBay);
            }
        }




        //Helper Method
        private static DockingBayReadDto ToReadDto(DockingBay dockingBay)
        {
            DockingBayReadDto outputDto = new DockingBayReadDto();
            outputDto.Id = dockingBay.Id;
            outputDto.BayNumber = dockingBay.BayNumber;
            outputDto.ShipId = dockingBay.ShipId;

            return outputDto;

        }
    }
}