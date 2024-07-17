using System.ComponentModel.DataAnnotations;

namespace ASUCloud.Web.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        [StringLength(128, ErrorMessage = "名前の長さは320文字以内です。")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(320, ErrorMessage = "メールアドレスの長さは320文字以内です。")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0}は少なくとも{2}文字でなければなりません。", MinimumLength = 8)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "パスワードは8文字以上で、大文字（A-Z）、小文字（a-z）、数字（0-9）、特殊文字 (例 !@#$%^&*)")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "パスワードが一致しません。")]
        public string ConfirmPassword { get; set; }
    }
}
