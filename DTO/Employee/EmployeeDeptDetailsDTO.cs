using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Employee
{
    public class EmployeeDeptDetailsDTO
    {
        public int EmployeeId { get; set; }

        public string EmployeeFirstName { get; set; }

        public string EmployeeLastName { get; set; }

        public string EmployeePosition { get; set; }
    }
}
