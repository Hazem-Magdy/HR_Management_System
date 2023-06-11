using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Employee
{
    public class EmployeeDeptDetailsDTO
    {
        [Required(ErrorMessage = "You must enter the first name.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name should only contain letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter the last name.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name should only contain letters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter the position.")]
        public string Position { get; set; }
    }
}
