using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.EntityFrameworkCore;

namespace HR_Management_System.Services.ClassesServices
{
    public class ProjectService : EntityBaseRepository<Project>, IProjectService
    {
        private readonly AppDbContext _context;
        public ProjectService(AppDbContext _db) : base(_db)
        {
            _context = _db;
        }
        public bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

        public async Task<Project> GetProjectByIdCustomAsync(int id)
        {

            Project existingProject = await _context.Projects
                .Include(p => p.projectPhases)
                .Include(p => p.projectTasks)
                .Include(p => p.Attendances)
                    .ThenInclude(a => a.Employee)
                .Include(p=>p.Employees)
                    .ThenInclude(e=>e.Employee)
                .FirstOrDefaultAsync(e => e.Id == id);

            return existingProject;
        }

        public async Task<Project> GetProjectByIdCustom2Async(int id)
        {
            Project existingProject = await _context.Projects
                .Include(p => p.Attendances)
                    .ThenInclude(a => a.Employee)
                .Include(p => p.Employees)
                    .ThenInclude(e => e.Employee)
                .FirstOrDefaultAsync(e => e.Id == id);
            return existingProject;
        }

        public Task<List<Project>> GetAllProjectsCustomAsync()
        {
            List<Project> ProjectList = _context.Projects
                .Include(p => p.projectPhases)
                .Include(p => p.projectTasks)
                .Include(p => p.Attendances)
                    .ThenInclude(a => a.Employee)
                .Include(p => p.Employees)
                    .ThenInclude(e => e.Employee).ToList();

            return Task.FromResult(ProjectList);
        }
    }
}
