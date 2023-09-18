using Microsoft.AspNetCore.Identity;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateAsync(IdentityUser user, RegisterModel model);

        Task<SignInResult> PasswordSignInAsync(LoginModel model);

        Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user);

        Task<IdentityUser> FindByIdAsync(string userId);

        Task<IdentityResult> ConfirmEmailAsync(IdentityUser user, string code);

        Task SignInAsync(IdentityUser user);

        Task SignOutAsync();

        Task DeleteUserAsync(IdentityUser user);
    }
}
