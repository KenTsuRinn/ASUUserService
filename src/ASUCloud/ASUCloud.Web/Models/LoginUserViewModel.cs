using System.ComponentModel.DataAnnotations;

namespace ASUCloud.Web.Models
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "メールが無効です。")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "パスワードが無効です。")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
