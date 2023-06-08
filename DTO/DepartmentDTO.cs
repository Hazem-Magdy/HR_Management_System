using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO
{
    public class DepartmentDTO
    {
        [Required(ErrorMessage = "You must enter the department name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must specify the manager ID.")]
        public int ManagerId { get; set; }

        public ICollection<int> EmployessIds { get; set; }
    }
}
