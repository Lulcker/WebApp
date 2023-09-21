using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateAsync(User user, RegisterModel model)
        {
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result;
        }

        public async Task SignInAsync(User user)
        {
            await _signInManager.SignInAsync(user, false);
        }

        public async Task<SignInResult> PasswordSignInAsync(LoginModel model)
        {
            await _signInManager.SignOutAsync();
            var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            if (!user.EmailConfirmed)
                await _userManager.DeleteAsync(user);
        }

        public async Task<bool> IsActiveUser(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Login);
            if (user?.UserStateId == 2)
                return false;
            else
                return true;
        }
    }
}
