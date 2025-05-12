using System.ComponentModel.DataAnnotations;

namespace IdentityChatEmail.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; }

        [MinLength(6, ErrorMessage = "Password must be least 6 length")]
        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
