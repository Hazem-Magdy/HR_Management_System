using HR_Management_System.Data.Base;
using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models
{
    public class ProjectPhase : IEntityBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must Enter the name of the Project phase")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name should only contains letters")]
        public ProjectPhases Name { get; set; }

        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartPhase { get; set; }

        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndPhase { get; set; }

        public int HoursBudget { get; set; }

        

        // navigation properties
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public ICollection<Attendance> Attendances = new HashSet<Attendance>();

    }
}
