using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class User : IdentityUser
    {
        public string? ReasonBlocking { get; set; }
    }
}
