using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Employee
{
    public class CreateEmployeeDTO
    {
        [Required(ErrorMessage = "You must Enter the name of the firstName")]
        [RegularExpression("^[a-zA-Z\\s]*$", ErrorMessage = "Name should only contains letters")]
        public string EmployeeFirstName { get; set; }

        [Required(ErrorMessage = "You must Enter the name of the lastName")]
        [RegularExpression("^[a-zA-Z\\s]*$", ErrorMessage = "Name should only contains letters")]
        public string EmployeeLastName { get; set; }

        [Required(ErrorMessage = "You must Enter the Salary per hours")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Salary per hours should only contains numbers")]
        public decimal EmployeeSalaryPerHour { get; set; }

        [Required(ErrorMessage = "You must Enter the over time rate")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Over time rate should only contains numbers")]
        public decimal EmployeeOvertimeRate { get; set; }

        [Required(ErrorMessage = "You must Enter the regular hours per day")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Regular hours per day should only contains numbers")]
        public decimal EmployeeRegularHoursPerDay { get; set; }

        [Required(ErrorMessage = "You must Enter the number of Working days per week")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Working days per week should only contains numbers")]
        public int EmployeeWorkingDaysPerWeek { get; set; }

        [RegularExpression(@"\.(jpg|png|jpeg)$", ErrorMessage = "Invalid profile URL format.")]
        public string? EmployeeProfileUrl { get; set; }

        //public IFormFile? EmployeeProfileUrl { get; set; }

        [Required(ErrorMessage = "You must Enter the phone number")]
        [RegularExpression("^(010|012|011|015)\\d{8}$", ErrorMessage = "The format is not supported, it should start with on of the following, {010,012,011,015}")]
        public string EmployeePhone { get; set; }

        [Required(ErrorMessage = "You must Enter the Email")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Enter a valid Email")]
        public string EmployeeEmail { get; set; }

        [Required(ErrorMessage = "You must Enter the Password")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&\\s])[A-Za-z\\d@$!%*#?&\\s]{8,}$", ErrorMessage = "Password must enter at least one Upper case letter, degit, special chaaracters, and at least 8 length")]
        public string EmployeePassword { get; set; }

        [Required(ErrorMessage = "You must Enter the Position")]
        [EnumDataType(typeof(EmployeePositions))]
        public EmployeePositions EmployeePosition { get; set; }

        [Required(ErrorMessage = "You must Enter the hiring date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EmployeeHiringDate { get; set; }

        [Required(ErrorMessage = "You must Enter the status")]
        [EnumDataType(typeof(Status))]
        public Status EmployeeStatus { get; set; }
    }
}
