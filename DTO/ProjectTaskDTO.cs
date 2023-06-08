using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO
{
    public class ProjectTaskDTO
    {
        [Required(ErrorMessage = "You must enter the name of the task.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name should only contain letters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must enter the description.")]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d).+$", ErrorMessage = "Description should contain at least one letter and one digit.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must enter the total hours per task.")]
        [Range(0, int.MaxValue, ErrorMessage = "Total hours per task must be a non-negative value.")]
        public int TotalHoursPerTask { get; set; }

        [Required(ErrorMessage = "You must specify the project ID.")]
        public int ProjectId { get; set; }
    }
}
