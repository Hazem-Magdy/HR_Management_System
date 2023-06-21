using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Employee
{
    public class UpdateEmployeeDTO
    {

        
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name should only contain letters.")]
        public string EmployeeFirstName { get; set; }

       
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name should only contain letters.")]
        public string EmployeeLastName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary per hour must be a non-negative value.")]
        public decimal EmployeeSalaryPerHour { get; set; }

        public decimal EmployeeOvertimeRate { get; set; }
        public decimal EmployeeRegularHoursPerDay { get; set; }
        public int EmployeeWorkingDaysPerWeek { get; set; }

        [RegularExpression(@"\.(jpg|png|jpeg)$", ErrorMessage = "Invalid profile URL format.")]
        public string? EmployeeProfileUrl { get; set; }

        [RegularExpression("^(010|012|011|015)\\d{8}$", ErrorMessage = "Invalid phone number format.")]
        public string EmployeePhone { get; set; }

        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format.")]
        public string EmployeeEmail { get; set; }

        public string EmployeePassword { get; set; }

        public EmployeePositions EmployeePosition { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EmployeeHiringDate { get; set; }

        [EnumDataType(typeof(Status))]
        public Status EmployeeStatus { get; set; }

        public int? DepartmentId { get; set; }
    }
}
