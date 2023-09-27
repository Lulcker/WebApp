using WebApp.ViewModels;
using WebApp.Models;

namespace WebApp.Repositories
{
    public interface IPostRepository
    {

        Task<IndexViewModel> GetAllPostAsync(int pageCount, int pageNumber, int pageSize = 5);

        Task<Post> GetPostAsync(int id);

        Task<int> PostCount();

        Task UpdatePostAsync(PostModel model);

        Task AddPostAsync(PostModel model);
    }
}
