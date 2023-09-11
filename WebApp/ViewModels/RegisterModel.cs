using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [MinLength(3, ErrorMessage = "Минимальная длина логина: 3")]
        [MaxLength(20, ErrorMessage = "Максимальная длина логина: 20")]
        [Display(Name = "Логин")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [Display(Name = "Почта")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [MinLength(3, ErrorMessage = "Минимальная длина пароля: 8")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [MinLength(3, ErrorMessage = "Минимальная длина пароля: 8")]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string? ComfirmPassword { get; set; }
    }
}
