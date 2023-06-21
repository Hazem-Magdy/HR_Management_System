using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.EntityFrameworkCore;

namespace HR_Management_System.Services.ClassesServices
{
    public class EmployeeProjectsService : EntityBaseRepository<Models.EmployeeProject>, IEmployeeProjectsService
    {
        private readonly AppDbContext db;

        public EmployeeProjectsService(AppDbContext _db) : base(_db)
        {
            db = _db;
        }
        public async Task<IEnumerable<EmployeeProject>> GetAllEmployeesCustom(int projectId)
        {
            IEnumerable<EmployeeProject> employeeProjectsList = await db.EmployeeProjects.Where(e => e.ProjectId == projectId).ToListAsync();
            return employeeProjectsList;
        }
        public async Task<EmployeeProject> GetEmployeeProjectCustomAsync(int projectId, int employeeId)
        {
            EmployeeProject employeeProject = await db.EmployeeProjects.FirstOrDefaultAsync(e => e.ProjectId == projectId && e.EmployeeId == employeeId);
            return employeeProject;
        }
        public async Task DeleteEmplyeeProjectCustom(int projectId,int employeeId)
        {
            var employeeProjectExist = await GetEmployeeProjectCustomAsync(projectId, employeeId);
            db.EmployeeProjects.Remove(employeeProjectExist);
            db.SaveChanges();
        }
    }
}
