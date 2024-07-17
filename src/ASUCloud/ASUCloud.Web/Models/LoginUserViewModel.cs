using System.ComponentModel.DataAnnotations;

namespace ASUCloud.Web.Models
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Email is invalid.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is invalid.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
