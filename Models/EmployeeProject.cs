using HR_Management_System.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models
{
    public class EmployeeProject : IEntityBase
    {
        
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int ProjectPhaseId { get; set; }
        public ProjectPhase ProjectPhase { get; set; }

        [Required(ErrorMessage = "You must specify the hours spent on the project phase")]
        public int HoursSpent { get; set; }

       
        
    }
}
