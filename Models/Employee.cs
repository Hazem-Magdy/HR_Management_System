﻿using HR_Management_System.Data.Base;
using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models
{
    public class Employee : IEntityBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must Enter the name of the firstName")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name sould only contains letters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "You must Enter the name of the lastName")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name sould only contains letters")]
        public string LastName { get; set; }
        public decimal SalaryPerHour { get; set; }
        public decimal OvertimeRate { get; set; }
        public decimal RegularHoursPerDay { get; set; }
        public int WorkingDaysPerWeek { get; set; }

        [RegularExpression(@"\.(jpg|png|jpeg)$")]
        public string? ProfileUrl { get; set; }
        [RegularExpression("^(010|012|011|015)\\d{8}$")]
        public string Phone { get; set; }
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
        public string Email { get; set; }
        public string Password { get; set; }
        [EnumDataType(typeof(EmployeePositions))]
        public EmployeePositions Position { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HiringDate { get; set; }

        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        public ICollection<Attendance> Attendances = new HashSet<Attendance>();

        public virtual int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public ICollection<EmployeeProject> Projects = new HashSet<EmployeeProject>();

        public decimal CalculateSalaryPerPeriod(decimal overTime, decimal regularHours)
        {
           
            decimal regularSalary = regularHours * SalaryPerHour;
            decimal overtimeSalary = overTime * OvertimeRate;

            decimal totalSalary = regularSalary + overtimeSalary;
            return totalSalary;
        }
        public decimal GetRegularHours(int numberOfDays)
        {
            decimal regularHours = RegularHoursPerDay * numberOfDays;

            return regularHours;
        }
        public decimal CalculateSalaryPerProject(decimal totalHoursSpent)
        {
            decimal totalCost = totalHoursSpent * SalaryPerHour;

            return totalCost;
        }
    }
}
