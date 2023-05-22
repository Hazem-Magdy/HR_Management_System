using HR_Management_System.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO
{
    public class RegisterEmployeeDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public decimal Salary { get; set; }
        public string? ProfileUrl { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }

        public string Password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HiringDate { get; set; }
        public Status Status { get; set; }
    }
}
