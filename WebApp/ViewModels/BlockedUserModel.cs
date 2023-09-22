using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class BlockedUserModel
    {
        [HiddenInput]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [Display(Name = "Время блокировки пользователя:")]
        public DateTime LockoutEnd { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [Display(Name = "Причина блокировки пользователя:")]
        public string? ReasonBlocking { get; set; }
    }
}
