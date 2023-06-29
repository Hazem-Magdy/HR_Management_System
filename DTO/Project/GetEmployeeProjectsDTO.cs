using HR_Management_System.DTO.ProjectPhase;
using HR_Management_System.DTO.ProjectTask;

namespace HR_Management_System.DTO.Project
{
    public class GetEmployeeProjectsDTO
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<GetProjectPhasesForProjectDTO> ProjectPhases { get; set; }
        public List<GetProjectTasksForProjectDTO> ProjectTaskes { get; set; }
    }
}
