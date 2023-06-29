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
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "combination of uppercase and lowercase letters, numbers, and spaces")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must Enter the total budget")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Total budget should only contains numbers")]
        public decimal TotalBudget { get; set; }

        [Required(ErrorMessage = "You must Enter the hours budget")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Hours budget should only contains numbers")]
        public int HoursBudget { get; set; }

        [Required(ErrorMessage = "You must Enter the project status")]
        [EnumDataType(typeof(ProjectStatus))]
        public ProjectStatus ProjectStatus { get; set; }

        [Required(ErrorMessage = "You must Enter the name of the location")]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "you can use combination of uppercase and lowercase letters, numbers, and spaces")]
        public string Location { get; set; }

        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        
        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set;}

        [Required(ErrorMessage = "You must Enter the name of the description")]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "you can use combination of uppercase and lowercase letters, numbers, and spaces")]
        public string Description { get; set; }

        public ICollection<Attendance>? Attendances = new HashSet<Attendance>();

        public ICollection<ProjectPhase> projectPhases = new HashSet<ProjectPhase>();

        public ICollection<ProjectTask>? projectTasks = new List<ProjectTask>();

        public ICollection<EmployeeProject> Employees = new HashSet<EmployeeProject>();

    }
}
