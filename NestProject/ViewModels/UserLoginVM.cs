using System.ComponentModel.DataAnnotations;

namespace NestProject.ViewModels
{
    public class UserLoginVM
    {
        [Required, StringLength(25)]
        public string Username { get; set; }
        public string Email { get; set; }

        [Required, StringLength(40), DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
