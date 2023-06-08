using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;

namespace HR_Management_System.Services
{
    public class DepartmentService : EntityBaseRepository<Department>, IDepartmentService
    {
        public DepartmentService(AppDbContext _db) : base(_db) { }
    }
}
