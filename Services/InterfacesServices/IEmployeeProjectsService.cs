using HR_Management_System.Data.Base;
using HR_Management_System.Models;

namespace HR_Management_System.Services.InterfacesServices
{
    public interface IEmployeeProjectsService : IEntityBaseRepository<EmployeeProject>
    {
        public Task<IEnumerable<EmployeeProject>> GetAllEmployeesCustom(int projectId);
        public Task<EmployeeProject> GetEmployeeProjectCustomAsync(int projectId, int employeeId);

        public Task DeleteEmplyeeProjectCustom(int projectId, int employeeId);
    }
}
