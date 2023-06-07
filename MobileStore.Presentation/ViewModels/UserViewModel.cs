using MobileStore.Core.Models;

namespace MobileStore.Presentation.ViewModels
{
    public class UserViewModel
    {
        public UserModel User { get; init; } = null!;

        //public string? FirstName { get; set; }
        //public string? LastName { get; set;}
        //public string? Address { get; set;}

        //[Required(ErrorMessage = "Не указан Email")]
        //public string? Email { get; set;}
    }
}
