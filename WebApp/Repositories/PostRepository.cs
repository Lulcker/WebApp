using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationContext _db;
        public PostRepository(ApplicationContext db) 
        {
            _db = db;
        }

        public async Task AddPostAsync(AddPostModel model)
        {
            var Post = new Post
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
            };

            await _db.Posts.AddAsync(Post);
            await _db.SaveChangesAsync();
        }

        public async Task<IndexViewModel> GetAllPostAsync(int pageCount, int pageNumber, int pageSize = 5)
        {
            var posts = await _db.Posts
                                 .Where(x => x.Enabled == true)
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
            PagingInfo pagingInfo = new PagingInfo(pageCount, pageNumber, pageSize);
            return new IndexViewModel(posts, pagingInfo);
        }

        public async Task<Post> GetPostAsync(int id)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(x => x.Id == id);
            return post;
        }

        public async Task<int> PostCount()
        {
            int count = await _db.Posts.CountAsync();
            return count;
        }
    }
}
