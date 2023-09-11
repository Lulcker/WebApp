namespace WebApp.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Author { get; set; }

        public string? PathToImage { get; set; }

        public bool Enabled { get; set; }
    }
}
