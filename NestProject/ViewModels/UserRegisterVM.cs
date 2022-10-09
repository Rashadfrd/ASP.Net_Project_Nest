using System.ComponentModel.DataAnnotations;

namespace NestProject.ViewModels
{
    public class UserRegisterVM
    {
        [Required,StringLength(25)]
        public string Name { get; set; }

        [Required, StringLength(35)]
        public string Surname { get; set; }

        [Required, StringLength(25)]
        public string Username { get; set; }

        [Required, StringLength(45),DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, StringLength(45), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, StringLength(45), DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
