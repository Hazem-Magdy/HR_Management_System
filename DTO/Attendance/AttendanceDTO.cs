using HR_Management_System.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Attendance
{
    public class AttendanceDTO
    {
        
        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        public int ProjectPhaseId { get; set; }

        public int ProjectTaskId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int HoursSpent { get; set; }
    }
}
