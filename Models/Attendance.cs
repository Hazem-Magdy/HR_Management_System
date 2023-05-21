using HR_Management_System.Data.Base;

namespace HR_Management_System.Models
{
    public class Attendance : IEntityBase
    {
        public int Id { get; set; }
        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]    
        public DateTime Date { get; set; }
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }


    }
}
