using HR_Management_System.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace HR_Management_System.DTO
{
    public class AttendanceDTO
    {
        public string UserName { get; set; }

        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }

        public int ProjectPhaseId { get; set; }

        public int ProjectTaskId { get; set; }

        public int HoursSpent { get; set; }
    }
}
