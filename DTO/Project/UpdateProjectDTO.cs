using HR_Management_System.Data.Enums;
using HR_Management_System.DTO.ProjectPhase;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Project
{
    public class UpdateProjectDTO
    {
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "combination of uppercase and lowercase letters, numbers, and spaces")]
        public string ProjectName { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Total budget should only contains numbers")]
        public decimal ProjectTotalBudget { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Hours budget should only contains numbers")]
        public int ProjectHours { get; set; }

        [EnumDataType(typeof(ProjectStatus))]
        public ProjectStatus ProjectStatus { get; set; }

        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "you can use combination of uppercase and lowercase letters, numbers, and spaces")]
        public string ProjectLocation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProjectStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProjectEndDate { get; set; }

        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "you can use combination of uppercase and lowercase letters, numbers, and spaces")]
        public string ProjectDescription { get; set; }

        public ICollection<ProjectPhaseWithNoIdDTO> projectPhases { get; set; }

        public ICollection<int> EmployeesInProjectIds { get; set; }


    }
}
