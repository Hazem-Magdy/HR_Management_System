using HR_Management_System.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.Models
{
    public class User : IEntityBase
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


    }
}
