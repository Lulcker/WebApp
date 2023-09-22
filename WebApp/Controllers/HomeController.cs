using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;

        public HomeController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            var count = await _postRepository.PostCount();
            var posts = await _postRepository.GetAllPostAsync(count, page);
            return View(posts);
        }

        [HttpGet]
        public IActionResult CreatePost()
        {
            return View(EmptyModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostModel model)
        {
            await _postRepository.AddPostAsync(model);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [Route("/Home/{id:int}/Details")]
        public async Task<IActionResult> Details(int id) 
        {
            var post = await _postRepository.GetPostAsync(id);
            return View(post);
        }

        private CreatePostModel EmptyModel()
        {
            return new CreatePostModel
            {
                Author = User.Identity.Name
            };
        }
    }
}
