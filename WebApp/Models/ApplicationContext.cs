using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<UpdatePost> UpdatePosts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().Property(x => x.Enabled).HasDefaultValue(false);
            modelBuilder.Entity<Post>().Property(x => x.Update).HasDefaultValue(false);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "2c5e174e-3b1e-446f-86af-483d56fd7210", Name = "Admin" }
                );

            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7211",
                    UserName = "Admin",
                    Email = "admin@mail.ru",
                    NormalizedUserName = "Admin".ToUpper(),
                    NormalizedEmail = "admin@mail.ru".ToUpper(),
                    PasswordHash = hasher.HashPassword(user: null, "Admin123"),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "2c5e174e-3b1e-446f-86af-483d56fd7210",
                    UserId = "2c5e174e-3b0e-446f-86af-483d56fd7211"
                });
        }
    }
}
