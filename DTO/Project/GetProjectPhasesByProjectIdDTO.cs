using HR_Management_System.Data.Enums;

namespace HR_Management_System.DTO.Project
{
    public class GetProjectPhasesByProjectIdDTO
    {
        public ProjectPhases Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Milestone { get; set; }

        public int HrBudget { get; set; }
    }
}
