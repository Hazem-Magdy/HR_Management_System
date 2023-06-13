using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Employee
{
    public class EmployeeDeptDetailsDTO
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "You must enter the first name.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name should only contain letters.")]
        public string EmployeeFirstName { get; set; }

        [Required(ErrorMessage = "You must enter the last name.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name should only contain letters.")]
        public string EmployeeLastName { get; set; }

        [Required(ErrorMessage = "You must enter the position.")]
        public string EmployeePosition { get; set; }
    }
}
