using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
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

        [DataType(DataType.ImageUrl)]
        [HiddenInput]
        public string? PathToImage { get; set; }

        [DisplayName("Постер")]
        public IFormFile Image { get; set; }
    }
}