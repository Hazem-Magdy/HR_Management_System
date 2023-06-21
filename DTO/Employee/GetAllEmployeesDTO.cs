using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Employee
{
    public class GetAllEmployeesDTO
    {

        public int EmplyeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public decimal EmployeeSalaryPerHour { get; set; }
        public decimal EmployeeOvertimeRate { get; set; }
        public decimal EmployeeRegularHoursPerDay { get; set; }
        public int EmployeeWorkingDaysPerWeek { get; set; }
        public string EmployeeProfileUrl { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePosition { get; set; }
        public DateTime EmployeeHiringDate { get; set; }
        public string EmployeeStatus { get; set; }

        public int? DepartmentId { get; set; }
    }
}
