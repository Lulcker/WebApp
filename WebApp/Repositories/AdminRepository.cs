using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IImageRepository _imageRepository;
        public AdminRepository(ApplicationContext db, UserManager<User> userManager, IImageRepository imageRepository)
        {
            _db = db;
            _userManager = userManager;
            _imageRepository = imageRepository;
        }

        public async Task<IEnumerable<Post>> GetAllPostAsync()
        {
            var posts = await _db.Posts.ToListAsync();
            return posts;
        }

        public async Task<IEnumerable<UpdatePost>> GetAllUpdatePostAsync()
        {
            var posts = await _db.UpdatePosts
                                .Include(x=>x.Post)
                                .ToListAsync();
            return posts;
        }

        public async Task DeletePost(int id)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post != null)
            {
                _imageRepository.DeleteImage(Path.GetFileName(post.PathToImage));

                _db.Posts.Remove(post);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetNewPostAsync()
        {
            var posts = await _db.Posts.Where(x => x.Enabled == false).ToListAsync();
            return posts;
        }

        public async Task AcceptPostAsync(int id)
        {
            var post = _db.Posts.FirstOrDefault(x => x.Id == id);
            if (post != null)
            {
                post.Enabled = true;
                _db.Posts.Update(post);
                await _db.SaveChangesAsync();
            }
        }

        public async Task CancelPostAsync(int id)
        {
            var post = _db.Posts.FirstOrDefault(x => x.Id == id);
            if (post != null)
            {
                _db.Posts.Remove(post);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _db.Users.Where(u => u.UserName != "Admin").ToListAsync();
            return users;
        }

        public async Task BlockedUser(BlockedUserModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.ReasonBlocking = model.ReasonBlocking;
                user.LockoutEnd = model.LockoutEnd.ToUniversalTime();
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UnblockedUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.ReasonBlocking = "";
                user.LockoutEnd = null;
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task AcceptUpdatePost(int id)
        {
            var updatePost = await _db.UpdatePosts.FirstOrDefaultAsync(x => x.Id == id);
            if (updatePost != null)
            {
                var post = await _db.Posts.FirstOrDefaultAsync(x => x.Id == updatePost.PostId);
                if (post != null)
                {
                    if (post.PathToImage != updatePost.PathToImage)
                    {
                        _imageRepository.DeleteImage(Path.GetFileName(post.PathToImage));
                    }
                    
                    post.Title = updatePost.Title;
                    post.Description = updatePost.Description;
                    post.Content = updatePost.Content;
                    post.PathToImage = updatePost.PathToImage;
                    post.Update = false;


                    _db.Posts.Update(post);
                    _db.UpdatePosts.Remove(updatePost);
                    await _db.SaveChangesAsync();
                }
            }
        }

        public async Task CancelUpdatePost(int id)
        {
            var updatePost = await _db.UpdatePosts.FirstOrDefaultAsync(x => x.Id == id);
            if (updatePost != null)
            {
                var post = await _db.Posts.FirstOrDefaultAsync(x => x.Id == updatePost.PostId);
                if (post != null)
                {
                    post.Update = false;

                    if (post.PathToImage != updatePost.PathToImage)
                    {
                        _imageRepository.DeleteImage(Path.GetFileName(updatePost.PathToImage));
                    }

                    _db.Posts.Update(post);
                    _db.UpdatePosts.Remove(updatePost);
                    await _db.SaveChangesAsync();
                }
            }
        }

        public async Task Update(PostModel updatePost)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(x => x.Id == updatePost.Id);
            if (post != null)
            {
                if (updatePost.Image != null)
                {
                    string extension = Path.GetExtension(updatePost.Image.FileName);
                    updatePost.PathToImage = await _imageRepository.SaveImageAsync(extension, updatePost.Image.OpenReadStream());

                    _imageRepository.DeleteImage(Path.GetFileName(post.PathToImage));
                }
                

                post.Title = updatePost.Title;
                post.Description = updatePost.Description;
                post.Content = updatePost.Content;
                post.PathToImage = updatePost.PathToImage;

                _db.Posts.Update(post);
                await _db.SaveChangesAsync();
            }
        }
    }
}