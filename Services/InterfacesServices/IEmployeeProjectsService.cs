using HR_Management_System.Data.Base;
using HR_Management_System.Models;

namespace HR_Management_System.Services.InterfacesServices
{
    public interface IEmployeeProjectsService : IEntityBaseRepository<EmployeeProject>
    {
        public Task<IEnumerable<EmployeeProject>> GetAllEmployeesCustom(int projectId);
    }
}
