using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;

namespace HR_Management_System.Services
{
    public class ProjectService : EntityBaseRepository<Project>, IProjectService
    {
        public ProjectService(ITIDbContext _db) : base(_db) { }
    }
}
