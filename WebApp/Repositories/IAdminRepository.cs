using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.Repositories
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Post>> GetAllPostAsync();

        Task AcceptPostAsync(int id);

        Task CancelPostAsync(int id);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<IEnumerable<Post>> GetNewPostAsync();

        Task BlockedUser(string id);

        Task UnblockedUser(string id);
    }
}
