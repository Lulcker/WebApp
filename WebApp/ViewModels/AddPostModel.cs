using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class AddPostModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        public string? Author { get; set; }
    }
}