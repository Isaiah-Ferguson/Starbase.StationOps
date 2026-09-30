using Starbase.StationOps.DTOs;

namespace Starbase.StationOps.Services
{
    public interface IMissionsService
    {
        List<MissionsReadDTO> GetAll();
        MissionsReadDTO? GetById(int id);
        MissionsReadDTO? Create(MissionsCreateDTO dto);
        bool Update(int id, MissionsCreateDTO dto);
        void Delete(int id);
    }
}