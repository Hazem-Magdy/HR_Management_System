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

        public int HoursSpent { get; set; }

        public string Description { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }
        public int ProjectPhaseId { get; set; }

        public virtual ProjectPhase ProjectPhase { get; set; }

        public int ProjectTaskId { get; set; }

        public virtual ProjectTask ProjectTask { get; set; }


    }
}
