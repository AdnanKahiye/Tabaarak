using System.ComponentModel.DataAnnotations;

namespace Tabaarak.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Identifier { get; set; }  // This can be either a username or an email

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
