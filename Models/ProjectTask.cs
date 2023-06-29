using HR_Management_System.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models
{
    public class ProjectTask:IEntityBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must Enter the name of the task")]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "Name should only contains letters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must Enter the name of the description")]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d).+$", ErrorMessage = "Name should only contains letters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must Enter the total hours per task")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Total hours per task budget should only contains numbers")]
        public int ToltalHoursPerTask { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public ICollection<Attendance> Attendances = new HashSet<Attendance>();

    }
}
