using WebApp.Models;

namespace WebApp.ViewModels
{
    
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; }
        public PagingInfo PagingInfo { get; }

        public IndexViewModel(IEnumerable<Post> posts, PagingInfo pagingInfo)
        {
            Posts = posts;
            PagingInfo = pagingInfo;
        }
    }
}
