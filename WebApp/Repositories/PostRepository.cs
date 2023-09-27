using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationContext _db;
        private readonly IImageRepository _imageRepository;
        public PostRepository(ApplicationContext db, IImageRepository imageRepository)
        {
            _db = db;
            _imageRepository = imageRepository;
        }

        public async Task AddPostAsync(PostModel model)
        {
            string extension = Path.GetExtension(model.Image.FileName);
            model.PathToImage = await _imageRepository.SaveImageAsync(extension, model.Image.OpenReadStream());

            var Post = new Post
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                PathToImage = model.PathToImage
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

        public async Task UpdatePostAsync(PostModel model)
        {
            string? extension = Path.GetExtension(model.Image?.FileName);
            if (extension != null)
            {
                model.PathToImage = await _imageRepository.SaveImageAsync(extension, model.Image.OpenReadStream());
            }
            

            var updatePost = new UpdatePost
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                PathToImage = model.PathToImage
            };

            var post = await _db.Posts.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (post != null)
            {
                post.Update = true;
                post.UpdatePost = updatePost;
                _db.Posts.Update(post);
                await _db.UpdatePosts.AddAsync(updatePost);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<int> PostCount()
        {
            int count = await _db.Posts.CountAsync();
            return count;
        }
    }
}
