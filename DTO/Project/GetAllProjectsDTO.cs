using HR_Management_System.Data.Enums;
using HR_Management_System.DTO.Attendance;
using HR_Management_System.DTO.Employee;
using HR_Management_System.DTO.ProjectPhase;
using HR_Management_System.DTO.ProjectTask;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Project
{
    public class GetAllProjectsDTO
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "You must enter the name of the project")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name should only contain letters")]
        public string ProjectName { get; set; }

        public decimal ProjectTotalBudget { get; set; }

        public int ProjectHours { get; set; }

        public ProjectStatus ProjectStatus { get; set; }

        public string ProjectLocation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProjectStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProjectEndDate { get; set; }

        public string ProjectDescription { get; set; }

        public ICollection<GetAttendancesInProjectDTO> ProjectAttendances { get; set; }

        public ICollection<ProjectPhaseWithIdDTO> ProjectPhases { get; set; }

        public ICollection<ProjectTaskWithIdDTO> ProjectTasks { get; set; }

        public ICollection<EmployeeDeptDetailsDTO> EmployeesInProject { get; set; }
    }
}
