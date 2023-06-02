using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services
{
    public interface IAccountService
    {
        Task<UserModel?> GetUserByEmail(string userEmail);
        Task<bool> IsValidPassword(string userEmail, string password);
        Task<UserModel> RegisterUser(UserModel model);
    }
}
