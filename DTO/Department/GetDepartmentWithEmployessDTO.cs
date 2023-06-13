using HR_Management_System.DTO.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Management_System.DTO.Department
{
    public class GetDepartmentWithEmployessDTO
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string ManagerName { get; set; }
        public int? NoEmployees { get; set; }
        public ICollection<EmployeeDeptDetailsDTO>? Employees { get; set; }
    }
}
