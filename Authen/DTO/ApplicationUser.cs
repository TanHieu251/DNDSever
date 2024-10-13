using DNDServer.DTO.Request;
using Microsoft.AspNetCore.Identity;

namespace DNDServer.Authen.Request
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; } = null;
        public ICollection<Order>? Orders { get; set; }
    }
}
