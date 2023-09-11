using Microsoft.AspNetCore.Identity;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user, RegisterModel model)
        {
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async Task<IdentityUser> FindByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(IdentityUser user, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result;
        }

        public async Task SignInAsync(IdentityUser user)
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
    }
}
