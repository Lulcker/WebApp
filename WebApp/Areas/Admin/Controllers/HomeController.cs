using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        public HomeController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _adminRepository.GetNewPostAsync();
            return View(posts);
        }

        public async Task<IActionResult> UsersControl()
        {
            var users = await _adminRepository.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> AllPost()
        {
            var users = await _adminRepository.GetAllPostAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptPost(int id)
        {
            await _adminRepository.AcceptPostAsync(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CancelPost(int id)
        {
            await _adminRepository.CancelPostAsync(id);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BlockedUser(string id)
        {
            await _adminRepository.BlockedUser(id);
            return RedirectToAction("UsersControl", "Home");
        }

        public async Task<IActionResult> UnblockedUser(string id)
        {
            await _adminRepository.UnblockedUser(id);
            return RedirectToAction("UsersControl", "Home");
        }
    }
}
