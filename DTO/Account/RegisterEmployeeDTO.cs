using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Account
{
    public class RegisterEmployeeDTO
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string? EmployeeProfileUrl { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeEmail { get; set; }
        public decimal SalaryPerHour { get; set; }
        public decimal OvertimeRate { get; set; }
        public decimal RegularHoursPerDay { get; set; }
        public int WorkingDaysPerWeek { get; set; }
        public EmployeePositions EmployeePosition { get; set; }
        public string EmployeePassword { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EmployeeHiringDate { get; set; }
        public Status EmployeeStatus { get; set; }
    }
}
