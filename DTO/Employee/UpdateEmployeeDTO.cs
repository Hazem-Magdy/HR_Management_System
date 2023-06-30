using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Employee
{
    public class UpdateEmployeeDTO
    {


        [RegularExpression("^[a-zA-Z\\s]*$", ErrorMessage = "Name should only contains letters")]
        public string EmployeeFirstName { get; set; }

        
        [RegularExpression("^[a-zA-Z\\s]*$", ErrorMessage = "Name should only contains letters")]
        public string EmployeeLastName { get; set; }

        
        [RegularExpression("^[0-9]+$", ErrorMessage = "Salary per hours should only contains numbers")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary per hour must be a non-negative value.")]
        public decimal EmployeeSalaryPerHour { get; set; }

       
        [RegularExpression("^[0-9]+$", ErrorMessage = "Over time rate should only contains numbers")]
        public decimal EmployeeOvertimeRate { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Regular hours per day should only contains numbers")]
        public decimal EmployeeRegularHoursPerDay { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Working days per week should only contains numbers")]
        public int EmployeeWorkingDaysPerWeek { get; set; }

        [RegularExpression(@"\.(jpg|png|jpeg)$", ErrorMessage = "Invalid profile URL format.")]
        public string? EmployeeProfileUrl { get; set; }

        [RegularExpression("^(010|012|011|015)\\d{8}$", ErrorMessage = "The format is not supported, it should start with on of the following, {010,012,011,015}")]
        public string EmployeePhone { get; set; }

        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Enter a valid Email")]
        public string EmployeeEmail { get; set; }

        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&\\s])[A-Za-z\\d@$!%*#?&\\s]{8,}$", ErrorMessage = "Password must enter at least one Upper case letter, degit, special chaaracters, and at least 8 length")]
        public string EmployeePassword { get; set; }

        [EnumDataType(typeof(EmployeePositions))]
        public EmployeePositions EmployeePosition { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EmployeeHiringDate { get; set; }

        [EnumDataType(typeof(Status))]
        public Status EmployeeStatus { get; set; }

        public int? DepartmentId { get; set; }
    }
}
