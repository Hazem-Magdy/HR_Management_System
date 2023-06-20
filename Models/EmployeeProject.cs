using HR_Management_System.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Management_System.Models
{
    public class EmployeeProject : IEntityBase
    {
        //[NotMapped]
        public  int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
