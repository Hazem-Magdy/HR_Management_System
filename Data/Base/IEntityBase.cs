using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Data.Base
{
    public interface IEntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
