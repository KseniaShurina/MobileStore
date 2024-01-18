using System.ComponentModel.DataAnnotations;

namespace MobileStore.Presentation.Api.Models
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; } = null!;
    }
}
