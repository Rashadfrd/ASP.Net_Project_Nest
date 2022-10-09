using Microsoft.AspNetCore.Identity;

namespace NestProject.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
