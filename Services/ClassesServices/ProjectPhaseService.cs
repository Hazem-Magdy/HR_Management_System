using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.EntityFrameworkCore;


namespace HR_Management_System.Services.ClassesServices
{
    public class ProjectPhaseService : EntityBaseRepository<ProjectPhase>, IProjectPhaseService
    {
        private readonly AppDbContext context;
        public ProjectPhaseService(AppDbContext _db) : base(_db)
        {
            context = _db;
        }


        public async Task<IEnumerable<ProjectPhase>> getAllIncludeProjectAsync()
        {
            IEnumerable<ProjectPhase> projectPhases = await context.ProjectPhases.Include(pp => pp.Project).ToListAsync();

            return projectPhases;
        }
    }
}
