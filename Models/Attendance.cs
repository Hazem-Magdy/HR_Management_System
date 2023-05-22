using HR_Management_System.Data.Base;
using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models
{
    public class Attendance : IEntityBase
    {
        public int Id { get; set; }
        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Range(typeof(TimeSpan),"08:00","17:00", ErrorMessage ="Invalid time to login to the system.")]
        public TimeSpan TimeIn { get; set; }
        [Range(typeof(TimeSpan), "09:00", "22:00", ErrorMessage = "Invalid time value.")]
        public TimeSpan TimeOut { get; set; }
        [EnumDataType(typeof(AttendanceStatus))]
        public AttendanceStatus AttendanceStatus { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }


    }
}
