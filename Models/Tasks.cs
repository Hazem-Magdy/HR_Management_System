using HR_Management_System.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models
{
    public class Tasks:IEntityBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must Enter the name of the task")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name should only contains letters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must Enter the name of the description")]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d).+$", ErrorMessage = "Name should only contains letters")]
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
