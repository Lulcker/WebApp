using WebApp.Models;

namespace WebApp.ViewModels
{
    
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; }

        public IndexViewModel(IEnumerable<Post> posts)
        {
            Posts = posts;
        }
    }
}
