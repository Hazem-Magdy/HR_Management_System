using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Employee
{
    public class CreateEmployeeDTO
    {
        [Required(ErrorMessage = "You must enter the first name.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name should only contain letters.")]
        public string EmployeeFirstName { get; set; }

        [Required(ErrorMessage = "You must enter the last name.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name should only contain letters.")]
        public string EmployeeLastName { get; set; }

        [Required(ErrorMessage = "You must enter the salary per hour.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary per hour must be a non-negative value.")]
        public decimal EmployeeSalaryPerHour { get; set; }
        public decimal EmployeeOvertimeRate { get; set; }
        public decimal EmployeeRegularHoursPerDay { get; set; }
        public int EmployeeWorkingDaysPerWeek { get; set; }

        [RegularExpression(@"\.(jpg|png|jpeg)$", ErrorMessage = "Invalid profile URL format.")]
        public string? EmployeeProfileUrl { get; set; }

        [Required(ErrorMessage = "You must enter the phone number.")]
        [RegularExpression("^(010|012|011|015)\\d{8}$", ErrorMessage = "Invalid phone number format.")]
        public string EmployeePhone { get; set; }

        [Required(ErrorMessage = "You must enter the email address.")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format.")]
        public string EmployeeEmail { get; set; }

        [Required(ErrorMessage = "You must enter the password.")]
        public string EmployeePassword { get; set; }

        [Required(ErrorMessage = "You must enter the position.")]
        public string EmployeePosition { get; set; }

        [Required(ErrorMessage = "You must enter the hiring date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EmployeeHiringDate { get; set; }

        [Required(ErrorMessage = "You must enter the status.")]
        [EnumDataType(typeof(Status))]
        public Status EmployeeStatus { get; set; }
    }
}
