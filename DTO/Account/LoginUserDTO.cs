using System.ComponentModel.DataAnnotations;

namespace HR_Management_System.DTO.Account
{
    public class LoginUserDTO
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
