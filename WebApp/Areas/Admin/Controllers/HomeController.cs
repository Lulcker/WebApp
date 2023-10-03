using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories;
using WebApp.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAdminCommandRepository _commandRepository;
        public HomeController(IAdminRepository adminRepository, IAdminCommandRepository commandRepository)
        {
            _adminRepository = adminRepository;
            _commandRepository = commandRepository;
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

        public async Task<IActionResult> AllUpdatePost()
        {
            var posts = await _adminRepository.GetAllUpdatePostAsync();
            return View(posts);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _commandRepository.DeletePost(id);
            return RedirectToAction("AllPost", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AcceptPost(int id)
        {
            await _commandRepository.AcceptPostAsync(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CancelPost(int id)
        {
            await _commandRepository.CancelPostAsync(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/Admin/Home/{id}/BlockedUser")]
        public IActionResult BlockedUser(string id)
        {
            var blockedModel = new BlockedUserModel { Id = id };
            return View(blockedModel);
        }

        [HttpPost]
        [Route("/Admin/Home/{id}/BlockedUser")]
        public async Task<IActionResult> BlockedUser(BlockedUserModel model)
        {
            await _commandRepository.BlockedUser(model);
            return RedirectToAction("UsersControl", "Home");
        }

        public async Task<IActionResult> UnblockedUser(string id)
        {
            await _commandRepository.UnblockedUser(id);
            return RedirectToAction("UsersControl", "Home");
        }

        public async Task<IActionResult> AcceptUpdatePost(int id)
        {
            await _commandRepository.AcceptUpdatePostAsync(id);
            return RedirectToAction("AllUpdatePost", "Home");
        }

        public async Task<IActionResult> CancelUpdatePost(int id)
        {
            await _commandRepository.CancelUpdatePostAsync(id);
            return RedirectToAction("AllUpdatePost", "Home");
        }
    }
}
