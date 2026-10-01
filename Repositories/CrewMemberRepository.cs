using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Starbase.StationOps.Data;
using Starbase.StationOps.Models;

namespace Starbase.StationOps.Repositories
{
    public class CrewMemberRepository : ICrewMemberRepository
    {
         private readonly AppDbContext _db; 

        public CrewMemberRepository(AppDbContext db)
        {
            _db = db; 
        }
        
        public List<CrewMember> GetAll()
        {
            return _db.CrewMembers.ToList(); 
        }//end of getall

        public CrewMember? GetById(int id)
        {
            return _db.CrewMembers.FirstOrDefault(c => c.Id == id); 
        }//end of getbyid
        public CrewMember Add(CrewMember newCrewMember)
        {
            _db.CrewMembers.Add(newCrewMember);
            _db.SaveChanges();
            return newCrewMember;
        }//end of add
        public void Update(CrewMember newCrewMember)
        {
            _db.SaveChanges();
        }//end of update
        public void Delete(CrewMember deleteCrewMember)
        {
            _db.CrewMembers.Remove(deleteCrewMember);
            _db.SaveChanges(); 
        }//end of delete

    }//end of class
}//end of namespace