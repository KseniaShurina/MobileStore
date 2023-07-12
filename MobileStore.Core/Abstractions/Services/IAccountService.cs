using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services
{
    public interface IAccountService
    {
        Task<UserModel?> GetUserByEmail(string email);
        Task<bool> IsValidPassword(string email, string password);
        Task<UserModel> RegisterUser(UserRegisterModel model);
    }
}
