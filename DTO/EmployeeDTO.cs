
using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models.DTOs
{
    public class EmployeeDTO
    {

        public int EmplyeeId { get; set; }

        [Required(ErrorMessage = "You must enter the first name.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name should only contain letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter the last name.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name should only contain letters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter the salary per hour.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary per hour must be a non-negative value.")]
        public decimal SalaryPerHour { get; set; }

        [Required(ErrorMessage = "You must enter the overtime.")]
        [Range(0, double.MaxValue, ErrorMessage = "Overtime must be a non-negative value.")]
        public decimal OverTime { get; set; }

        [Required(ErrorMessage = "You must enter the salary.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]
        public decimal Salary { get; set; }

        [RegularExpression(@"\.(jpg|png|jpeg)$", ErrorMessage = "Invalid profile URL format.")]
        public string? ProfileUrl { get; set; }

        [Required(ErrorMessage = "You must enter the phone number.")]
        [RegularExpression("^(010|012|011|015)\\d{8}$", ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "You must enter the email address.")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter the password.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must enter the position.")]
        public string Position { get; set; }

        [Required(ErrorMessage = "You must enter the hiring date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HiringDate { get; set; }

        [Required(ErrorMessage = "You must enter the status.")]
        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        public int? DepartmentId{ get; set; }
    }
}
