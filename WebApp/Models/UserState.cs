namespace WebApp.Models
{
    public class UserState
    {
        public int Id { get; set; }

        public string? Code { get; set; }

        public List<User> User { get; set; } = new List<User>();
    }
}
