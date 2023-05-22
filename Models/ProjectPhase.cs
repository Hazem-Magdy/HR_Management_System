﻿using HR_Management_System.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models
{
    public class ProjectPhase : IEntityBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must Enter the name of the Project phase")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name should only contains letters")]
        public string Name { get; set; }

        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartPhase { get; set; }

        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndPhase { get; set; }
        public int HoursWorked { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
        public ICollection<Tasks> Tasks { get; set; } = new HashSet<Tasks>();
    }
}
