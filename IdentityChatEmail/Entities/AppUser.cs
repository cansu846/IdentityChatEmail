using Microsoft.AspNetCore.Identity;

namespace IdentityChatEmail.Entities
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string ProfileImage { get; set; }
    }
}
