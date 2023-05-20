namespace HR_Management_System.Models
{
    public class EmployeeProject
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int ProjectPhaseId { get; set; }
        public ProjectPhase ProjectPhase { get; set; }

        public int HoursWorked { get; set; }
    }
}
