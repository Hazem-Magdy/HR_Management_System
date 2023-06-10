using HR_Management_System.Data.Base;
using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models
{
    public class ProjectPhase : IEntityBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter the name of the Project phase")]
        [EnumDataType(typeof(ProjectPhases), ErrorMessage = "Invalid Project phase")]
        public ProjectPhases Name { get; set; }

        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartPhase { get; set; }

        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndPhase { get; set; }

        public int HrBudget { get; set; }

        public string Milestone { get; set; }



        // navigation properties
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public ICollection<Attendance> Attendances = new HashSet<Attendance>();

    }
}
