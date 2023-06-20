namespace HR_Management_System.DTO.ProjectPhase
{
    public class GetAllProjectsPhasesDTO
    {
        public int PhaseId { get; set; }
        public string PhaseName { get; set; }

        public DateTime PhaseStartDate { get; set; }

        public DateTime PhaseEndDate { get; set; }

        public string PhaseMilestone { get; set; }

        public int PhaseHrBudget { get; set; }
        public string ProjectName { get; set; }
    }
}
