using HR_Management_System.Data.Base;
using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models
{
    public class Project : IEntityBase
    {
        public Project()
        {
            Attendances = null;
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "You must Enter the name of the project")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name should only contains letters")]
        public string Name { get; set; }
        public decimal TotalBudget { get; set; }

        public int HoursBudget { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public string Location { get; set; }

        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        
        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set;}

        public string Description { get; set; }

        public ICollection<Attendance>? Attendances = new HashSet<Attendance>();

        public ICollection<ProjectPhase> projectPhases = new HashSet<ProjectPhase>();

        public ICollection<ProjectTask>? projectTasks = new List<ProjectTask>();

        public ICollection<EmployeeProject> Employees = new HashSet<EmployeeProject>();

    }
}
