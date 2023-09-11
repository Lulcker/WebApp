using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _db;
        public AdminRepository(ApplicationContext db)
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

        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            var users = await _db.Users.ToListAsync();
            return users;
        }
    }
}
