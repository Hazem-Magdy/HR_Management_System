using HR_Management_System.Data.Base;
using HR_Management_System.Data;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;

namespace HR_Management_System.Services.ClassesServices
{
    public class EmployeeProjectService : EntityBaseRepository<EmployeeProject>, IEmployeeProjectService
    {
        public EmployeeProjectService(AppDbContext _db) : base(_db) { }
    }

}
