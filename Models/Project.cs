using HR_Management_System.Data.Base;

namespace HR_Management_System.Models
{
    public class Project : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public string Location { get; set; }
        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        
        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set;}
        public int Hours { get; set; }
        public ICollection<ProjectPhase> ProjectPhases { get; set; } = new HashSet<ProjectPhase>();
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }


    }
}
