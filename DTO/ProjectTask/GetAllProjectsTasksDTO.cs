using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.ProjectTask
{
    public class GetAllProjectsTasksDTO
    {
        public int TaskId { get; set; }
      
        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public int ToltalHoursPerTask { get; set; }

        public string ProjectName { get; set; }
    }
}
