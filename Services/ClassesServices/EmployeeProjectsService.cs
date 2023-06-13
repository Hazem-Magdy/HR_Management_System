using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Services.InterfacesServices;

namespace HR_Management_System.Services.ClassesServices
{
    public class EmployeeProjectsService : EntityBaseRepository<Models.EmployeeProject>, IEmployeeProjectsService
    {
        private readonly AppDbContext db;

        public EmployeeProjectsService(AppDbContext _db) : base(_db)
        {
            db = _db;
        }
    }
}
