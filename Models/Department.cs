using HR_Management_System.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Management_System.Models
{
    public class Department : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Manger_Id { get; set; }
        public ICollection<Employee>? Employees { get; set; }=new HashSet<Employee>();
    }
}
