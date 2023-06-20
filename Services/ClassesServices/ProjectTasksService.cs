using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.EntityFrameworkCore;

namespace HR_Management_System.Services.ClassesServices
{
    public class ProjectTasksService : EntityBaseRepository<ProjectTask>, IProjectTasksService
    {
        private readonly AppDbContext context;
        public ProjectTasksService(AppDbContext _db) : base(_db)
        {
            context = _db;
        }
        public async Task<IEnumerable<ProjectTask>> getAllIncludinProjectAsync()
        {
            IEnumerable<ProjectTask> projectTasks = await context.ProjectTasks.Include(p=>p.Project).ToListAsync();

            return projectTasks;
        }
    }
}
