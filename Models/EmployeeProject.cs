namespace HR_Management_System.Models
{
    public class EmployeeProject
    {
        public EmployeeProject()
        {
            TotalHoursInProject = 0;
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public int TotalHoursInProject { get; set; }

        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}
