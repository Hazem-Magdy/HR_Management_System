using HR_Management_System.Data.Base;

namespace HR_Management_System.Models
{
    public class Employee : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Salary { get; set; }
        public string ProfileUrl { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HiringDate { get; set; }
        public Status Status { get; set; }

        public ICollection<Attendance> Attendances = new HashSet<Attendance>();

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
