using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Account
{
    public class LoginUserDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
