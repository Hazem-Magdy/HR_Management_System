using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Attendance
{
    public class GetAttendancesInProjectDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public string ProjectName { get; set; }

        public string PhaseName { get; set; }

        public string TaskName { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int HoursSpent { get; set; }
    }
}
