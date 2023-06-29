using HR_Management_System.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Management_System.Models
{
    public class Department : IEntityBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must Enter the name of the department")]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "You can use combination of uppercase and lowercase letters, numbers, and spaces")]
        public string Name { get; set; }

        [ForeignKey("Employee")]
        [Column(name: "ManagerId")]
        public int? EmployeeId { get; set; } = null;

        public int? NoEmployees { get; set; } = 0;
        public Employee Employee { get; set; }
        public ICollection<Employee>? Employees { get; set; } = new HashSet<Employee>();
    }
}
