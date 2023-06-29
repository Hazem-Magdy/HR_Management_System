using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.ProjectPhase
{
    public class CreateUpdateProjectPhaseDTO
    {
        public int Id { get; set; }
        public ProjectPhases PhaseName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PhaseStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PhaseEndDate { get; set; }

        [Required(ErrorMessage = "You must Enter the name of the milestone")]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "you can use combination of uppercase and lowercase letters, numbers, and spaces")]
        public string PhaseMilestone { get; set; }

        [Required(ErrorMessage = "You must Enter the HR budget")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "HR budget should only contains numbers")]
        public int PhaseHrBudget { get; set; }
    }
}
