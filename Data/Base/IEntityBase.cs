using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Management_System.Data.Base
{
    public interface IEntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
