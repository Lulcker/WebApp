using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateAsync(User user, RegisterModel model);

        Task<SignInResult> PasswordSignInAsync(LoginModel model);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<User> FindByIdAsync(string userId);

        Task<IdentityResult> ConfirmEmailAsync(User user, string code);

        Task SignInAsync(User user);

        Task SignOutAsync();

        Task DeleteUserAsync(User user);

        Task<User> FindByNameAsync(LoginModel model);
    }
}
