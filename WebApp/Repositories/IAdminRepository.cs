using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Post>> GetAllPostAsync();

        Task<IEnumerable<Post>> GetNewPostAsync();

        Task<IEnumerable<UpdatePost>> GetAllUpdatePostAsync();

        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
