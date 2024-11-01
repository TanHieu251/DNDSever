using DNDServer.Model;
using Microsoft.AspNetCore.Identity;

namespace DNDServer.Authen.Request
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; } = null;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpriyTime { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
