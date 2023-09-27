namespace WebApp.Models
{
    public class UpdatePost
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Content { get; set; }

        public string? Author { get; set; }

        public string? PathToImage { get; set; }

        public int PostId { get; set; }

        public Post? Post { get; set; }
    }
}
