using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class CreatePostModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [Display(Name = "Название")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        public string? Author { get; set; }

        [DataType(DataType.ImageUrl)]
        [HiddenInput]
        public string? PathToImage { get; set; }

        [Display(Name = "Постер")]
        public IFormFile Image { get; set; }
    }
}