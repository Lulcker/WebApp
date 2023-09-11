using WebApp.ViewModels;
using WebApp.Models;

namespace WebApp.Repositories
{
    public interface IPostRepository
    {
        Task AddPostAsync(AddPostModel model);

        Task<IndexViewModel> GetAllPostAsync();

        Task<Post> GetPostAsync(int id);
    }
}
