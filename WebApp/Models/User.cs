using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class User : IdentityUser
    {
        public int UserStateId { get; set; }

        public UserState? UserState { get; set; }
    }
}
