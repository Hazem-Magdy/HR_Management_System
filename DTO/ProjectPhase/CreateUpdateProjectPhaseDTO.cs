using HR_Management_System.Data.Enums;

namespace HR_Management_System.DTO.ProjectPhase
{
    public class CreateUpdateProjectPhaseDTO
    {
        public int Id { get; set; }
        public ProjectPhases PhaseName { get; set; }

        public DateTime PhaseStartDate { get; set; }

        public DateTime PhaseEndDate { get; set; }

        public string PhaseMilestone { get; set; }

        public int PhaseHrBudget { get; set; }
    }
}
