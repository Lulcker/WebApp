using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _db;
        public AdminRepository(ApplicationContext db, UserManager<User> userManager, IImageRepository imageRepository)
        {
            _db = db;
        }

        public async Task<IEnumerable<Post>> GetAllPostAsync()
        {
            var posts = await _db.Posts.ToListAsync();
            return posts;
        }

        public async Task<IEnumerable<Post>> GetNewPostAsync()
        {
            var posts = await _db.Posts.Where(x => x.Enabled == false).ToListAsync();
            return posts;
        }

        public async Task<IEnumerable<UpdatePost>> GetAllUpdatePostAsync()
        {
            var posts = await _db.UpdatePosts
                                .Include(x=>x.Post)
                                .ToListAsync();
            return posts;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _db.Users.Where(u => u.UserName != "Admin").ToListAsync();
            return users;
        }
    }
}