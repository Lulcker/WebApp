using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Post>> GetAllPostAsync();

        Task AcceptPostAsync(int id);

        Task CancelPostAsync(int id);

        Task DeletePost(int id);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<IEnumerable<Post>> GetNewPostAsync();

        Task BlockedUser(BlockedUserModel model);

        Task UnblockedUser(string id);
    }
}
