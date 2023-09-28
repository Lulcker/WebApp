using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Post>> GetAllPostAsync();

        Task<IEnumerable<UpdatePost>> GetAllUpdatePostAsync();

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<IEnumerable<Post>> GetNewPostAsync();

        Task AcceptPostAsync(int id);

        Task CancelPostAsync(int id);

        Task DeletePost(int id);

        Task BlockedUser(BlockedUserModel model);

        Task UnblockedUser(string id);

        Task AcceptUpdatePost(int id);

        Task CancelUpdatePost(int id);

        Task Update(PostModel updatePost);
    }
}
