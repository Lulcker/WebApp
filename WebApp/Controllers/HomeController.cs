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
        private readonly IImageRepository _imageRepository;

        public HomeController(IPostRepository postRepository, IImageRepository imageRepository)
        {
            _postRepository = postRepository;
            _imageRepository = imageRepository;
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
        public async Task<IActionResult> CreatePost(AddPostModel model)
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

        private AddPostModel EmptyModel()
        {
            return new AddPostModel
            {
                Author = User.Identity.Name
            };
        }
    }
}
