using Microsoft.EntityFrameworkCore;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Services
{
    internal class AccountService : IAccountService
    {
        private readonly IDefaultContext _context;

        public AccountService(IDefaultContext context)
        {
            _context = context;
        }

        public async Task<UserModel?> GetUserByEmail(string userEmail)
        {
            if (userEmail is null) throw new ArgumentNullException(nameof(userEmail));

            var email = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            return email?.MapToModel(); // ????????????
        }

        public async Task<bool> IsValidPassword(string userEmail, string password)
        {
            if (userEmail is null) throw new ArgumentNullException(nameof(userEmail));

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail && u.Password == password);
            var result = (password == user?.Password) ? true : false;
            return result;
        }

        public async Task<UserModel> RegisterUser(UserModel model)
        {
            if (model == null) throw new ArgumentNullException();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                user = new User { Email = model.Email!, Password = model.Password! };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            return user.MapToModel();

        }
    }
}
