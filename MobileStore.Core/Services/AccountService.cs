using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;
using MobileStore.Core.Helpers;

namespace MobileStore.Core.Services
{
    internal class AccountService : IAccountService
    {
        private readonly IDefaultContext _context;

        public AccountService(IDefaultContext context)
        {
            _context = context;
        }

        public async Task<UserModel?> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());
            return user?.MapToModel();
        }

        public async Task<bool> IsValidPassword(string email, string password)
        {
            if (string.IsNullOrEmpty(email)) return false;
            if (string.IsNullOrEmpty(password)) return false;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());

            if (user == null) return false;

            var passwordHash = PasswordHelper.HashPassword(password, user.PasswordSalt);

            var result = passwordHash == user.PasswordHash;
            return result;
        }

        public async Task<UserModel> RegisterUser(UserRegisterModel model)
        {
            Guard.Against.Null(model);

            var email = model.Email.ToLower();

            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            if (user != null) throw new ArgumentException("User with these parameters already exist", $"{nameof(user)}");

            var salt = Guid.NewGuid().ToString();
            var passwordHash = PasswordHelper.HashPassword(model.Password, salt);

            user = new User
            {
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.MapToModel();
        }
    }
}
