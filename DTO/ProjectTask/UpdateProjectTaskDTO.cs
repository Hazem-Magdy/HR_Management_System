using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.ProjectTask
{
    public class UpdateProjectTaskDTO
    {
        [Required(ErrorMessage = "You must enter the name of the task.")]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "Name should only contain letters.")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "You must enter the description.")]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d).+$", ErrorMessage = "Description should contain at least one letter and one digit.")]
        public string TaskDescription { get; set; }

        [Required(ErrorMessage = "You must enter the total hours per task.")]
        [Range(0, int.MaxValue, ErrorMessage = "Total hours per task must be a non-negative value.")]
        public int TotalHoursPerTask { get; set; }
    }
}
