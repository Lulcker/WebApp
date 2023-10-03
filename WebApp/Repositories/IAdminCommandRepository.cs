using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public interface IAdminCommandRepository
    {
        Task DeletePost(int id);

        Task AcceptPostAsync(int id);

        Task CancelPostAsync(int id);

        Task AcceptUpdatePostAsync(int id);

        Task CancelUpdatePostAsync(int id);

        Task BlockedUser(BlockedUserModel model);

        Task UnblockedUser(string id);
    }
}
