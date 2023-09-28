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
            var postModel = new PostModel
            {
                Author = User.Identity?.Name
            };
            return View(postModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostModel model)
        {
            await _postRepository.AddPostAsync(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/Home/{id:int}/UpdatePost")]
        public async Task<IActionResult> UpdatePost(int id)
        {
            var post = await _postRepository.GetPostAsync(id);
            var postModel = new PostModel
            {
                Id = id,
                Title = post.Title,
                Author = post.Author,
                Description = post.Description,
                Content = post.Content,
                PathToImage = post.PathToImage
            };
            return View(postModel);
        }

        [HttpPost]
        [Route("/Home/{id:int}/UpdatePost")]
        public async Task<IActionResult> UpdatePost(PostModel model)
        {
            await _postRepository.UpdatePostAsync(model);
            return RedirectToAction("Index", "Home");
        }

        

        [AllowAnonymous]
        [Route("/Home/{id:int}/Details")]
        public async Task<IActionResult> Details(int id) 
        {
            var post = await _postRepository.GetPostAsync(id);
            return View(post);
        }
    }
}
