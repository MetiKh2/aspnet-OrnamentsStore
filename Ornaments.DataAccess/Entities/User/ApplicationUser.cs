using Microsoft.AspNetCore.Identity;

namespace Ornaments.DataAccess.Entities.User
{
    public class ApplicationUser:IdentityUser
    {
        public bool IsAdmin { get; set; } = false;
    }
}
