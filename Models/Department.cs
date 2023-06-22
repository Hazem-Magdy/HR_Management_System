using HR_Management_System.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Management_System.Models
{
    public class Department : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Employee")]
        [Column(name: "ManagerId")]
        public int? EmployeeId { get; set; } = null;

        public int? NoEmployees { get; set; } = 0;
        public Employee Employee { get; set; }
        public ICollection<Employee>? Employees { get; set; } = new HashSet<Employee>();
    }
}
