using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.Repositories
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Post>> GetAllPostAsync();

        Task AcceptPostAsync(int id);

        Task CancelPostAsync(int id);

        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();

        Task<IEnumerable<Post>> GetNewPostAsync();
    }
}
