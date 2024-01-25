using Microsoft.AspNetCore.Identity;

namespace Carvilla.Models.Common
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
