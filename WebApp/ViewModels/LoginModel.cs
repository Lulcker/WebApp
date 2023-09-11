using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [Display(Name = "Введите логин")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [Display(Name = "Введите пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
