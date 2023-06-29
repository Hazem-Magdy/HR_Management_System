using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Department
{
    public class DepartmentDTO
    {
        [Required(ErrorMessage = "You must Enter the name of the department")]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "combination of uppercase and lowercase letters, numbers, and spaces")]
        public string DepartmentName { get; set; }

        public int? ManagerId { get; set; } = null;

        public ICollection<int> EmployessIds { get; set; }
    }
}
