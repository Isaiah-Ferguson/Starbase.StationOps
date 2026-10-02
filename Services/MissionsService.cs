using Microsoft.EntityFrameworkCore.Diagnostics;
using Starbase.StationOps.DTOs;
using Starbase.StationOps.Models;
using Starbase.StationOps.Repositories;
namespace Starbase.StationOps.Services
{
    public class MissionsService : IMissionsService 
    {
        private readonly IMissionsRepository _mission;
        private readonly IMaintenanceTicketRepository _maintenance; 
        public MissionsService(IMissionsRepository mission, IMaintenanceTicketRepository maintencance)
        {
            _mission = mission;
            _maintenance = maintencance; 
        }

        public List<MissionsReadDTO> GetAll()
        {
            return _mission.GetAll()
                .OrderBy(s => s.Title)
                .Select(ToReadDTO) // Turn every model into a DTO.
                .ToList();
        }

        public MissionsReadDTO? GetById(int id)
        {
            Missions? missions = _mission.GetById(id);

            if (missions is null)
            {
                return null;
            }

            return ToReadDTO(missions);
        }

        public MissionsReadDTO? Create(MissionsCreateDTO dto)
        {
            bool exists = _mission.GetAll().Any(s => string.Equals(s.Title, dto.Title, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                return null;
            }

            Missions missions = new Missions();
            missions.Title = dto.Title;
            missions.ShipId = dto.ShipId;
            missions.Status = dto.Status;

            Missions created = _mission.Add(missions);

            return ToReadDTO(created);
        }

        public bool Update(int id, MissionsCreateDTO dto)
        {
            Missions? existingMission = _mission.GetById(id);

            if (existingMission is null ||
                string.Equals(existingMission.Status, "Complete", StringComparison.OrdinalIgnoreCase) ||
                !IsValid(dto))
            {
                return false;
            }

            existingMission.Title = dto.Title;
            existingMission.ShipId = dto.ShipId;
            existingMission.Status = dto.Status;

            _mission.Update(existingMission);
            return true;
        }

        public void Delete(int id)
        {
            Missions? supply = _mission.GetById(id);
            // if not null (i.e is found when ID is entered, will delete record)
            if (supply != null)
            {
                _mission.Delete(supply);
            }
        }
        public MissionsReadDTO Advance(int id)
        {
            
            Missions? mission = _mission.GetById(id); 
            
            if(mission is null)
            {
                return null;
            }
        //      MaintenanceTicket? resolved = _maintenance.GetAll().FirstOrDefault(m => m.ShipId == mission.ShipId);
        //    if(resolved is null)
        //     {
        //         return null; 
        //     }
        //     if(resolved.IsResolved == false)
        //     {
        //         return null; 
        //     }
            if(mission.Status == "Planned")
            {
                mission.Status = "Active"; 
            }
             else if(mission.Status == "Active")
            {
                mission.Status = "Complete"; 
            }
            
            _mission.Update(mission); 
            return ToReadDTO(mission); 
        }

        public List<MissionsReadDTO>? GetMissions(int shipId)
        {
              List<Missions>? mission = _mission.GetAll().Where(s => s.ShipId == shipId).ToList(); 
            
            if(mission is null)
            {
                return null;
            }
            return _mission.GetAll().Where(ss => ss.ShipId == shipId).Select(ToReadDTO).ToList(); 

            //   return _mission.GetAll()
            //     .OrderBy(s => s.Title)
            //     .Select(ToReadDTO) // Turn every model into a DTO.
            //     .ToList();
        }
        private static MissionsReadDTO ToReadDTO(Missions missions)
        {
            MissionsReadDTO outputDTO = new MissionsReadDTO();
            outputDTO.Id = missions.Id;
            outputDTO.Title = missions.Title;
            outputDTO.ShipId = missions.ShipId;
            outputDTO.Status = missions.Status;

            return outputDTO;

        }
        private static bool IsValid(MissionsCreateDTO dto)
        {
            if (dto is null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                return false;
            }

            return true;
        }
    }
}