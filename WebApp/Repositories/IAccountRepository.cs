using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateAsync(User user, RegisterModel model);

        Task<IdentityResult> ConfirmEmailAsync(User user, string code);

        Task<SignInResult> PasswordSignInAsync(LoginModel model);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<User> FindByIdAsync(string userId);

        Task<User> FindByNameAsync(LoginModel model);

        Task SignInAsync(User user);

        Task DeleteUserAsync(User user);

        Task SignOutAsync();
    }
}
