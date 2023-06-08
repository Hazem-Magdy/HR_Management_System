namespace HR_Management_System.DTO
{
    public class EmployeeProjectDTO
    {
        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        public int ProjectPhaseId { get; set; }

        public decimal HoursSpent {get; set;}
    }
}
