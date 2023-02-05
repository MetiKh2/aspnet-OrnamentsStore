using Microsoft.AspNetCore.Identity;
using Ornaments.DataAccess.Entities.Order;

namespace Ornaments.DataAccess.Entities.User
{
    public class ApplicationUser:IdentityUser
    {
        public bool IsAdmin { get; set; } = false;
        public ICollection<Order.Order> Orders{ get; set; }
    }
}
