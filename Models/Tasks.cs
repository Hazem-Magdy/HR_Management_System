using HR_Management_System.Data.Base;

namespace HR_Management_System.Models
{
    public class Tasks:IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
