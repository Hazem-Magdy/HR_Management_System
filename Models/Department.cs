using HR_Management_System.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models
{
    public class Department : IEntityBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="You must Enter your name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage ="Name should only contains letters")]
        public string Name { get; set; }
        public int Manger_Id { get; set; }
        public ICollection<Employee> Employees { get; set; }=new HashSet<Employee>();

    }
}
