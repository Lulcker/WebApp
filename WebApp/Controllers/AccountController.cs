using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;

        public AccountController(IAccountRepository accountRepository, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.Login,
            };

            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateAsync(user, model);
                if (result.Succeeded)
                {
                    var code = await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: Request.Scheme
                        );
                    await _emailService.SendEmailAsync(model.Email, "Подтверждение почты",
                        $"Подтвердите регистрацию, перейдя по <a href='{callbackUrl}'>ссылке</a>");

                    BackgroundJob.Schedule(() => _accountRepository.DeleteUserAsync(user), TimeSpan.FromMinutes(15));
                    return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _accountRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _accountRepository.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                await _accountRepository.SignInAsync(user);
                return RedirectToAction("Index", "Home");
            }
            else
                return View("Error");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountRepository.FindByNameAsync(model);
                if (user?.LockoutEnd == null)
                {
                    var result = await _accountRepository.PasswordSignInAsync(model);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return LocalRedirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неверный логин или пароль");
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"Аккаунт заблокирован до {user.LockoutEnd.Value.ToString("D")} по причине: {user.ReasonBlocking}");
                }
                
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
