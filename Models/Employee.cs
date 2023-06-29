using HR_Management_System.Data.Base;
using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models
{
    public class Employee : IEntityBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must Enter the name of the firstName")]
        [RegularExpression("^[a-zA-Z\\s]*$", ErrorMessage = "Name should only contains letters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must Enter the name of the lastName")]
        [RegularExpression("^[a-zA-Z\\s]*$", ErrorMessage = "Name should only contains letters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must Enter the Salary per hours")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Salary per hours should only contains numbers")]
        public decimal SalaryPerHour { get; set; }

        [Required(ErrorMessage = "You must Enter the over time rate")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Over time rate should only contains numbers")]
        public decimal OvertimeRate { get; set; }

        [Required(ErrorMessage = "You must Enter the regular hours per day")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Regular hours per day should only contains numbers")]
        public decimal RegularHoursPerDay { get; set; }

        [Required(ErrorMessage = "You must Enter the number of Working days per week")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Working days per week should only contains numbers")]
        public int WorkingDaysPerWeek { get; set; }

        [RegularExpression(@"\.(jpg|png|jpeg)$", ErrorMessage ="The format is not supported")]
        public string? ProfileUrl { get; set; }

        [Required(ErrorMessage = "You must Enter the phone number")]
        [RegularExpression("^(010|012|011|015)\\d{8}$", ErrorMessage = "The format is not supported, it should start with on of the following, {010,012,011,015}")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "You must Enter the Email")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage ="Enter a valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must Enter the Password")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&\\s])[A-Za-z\\d@$!%*#?&\\s]{8,}$", ErrorMessage ="Password must enter at least one Upper case letter, degit, special chaaracters, and at least 8 length")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must Enter the Position")]
        [EnumDataType(typeof(EmployeePositions))]
        public EmployeePositions Position { get; set; }

        [Required(ErrorMessage = "You must Enter the hiring date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HiringDate { get; set; }

        [Required(ErrorMessage = "You must Enter the status")]
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
