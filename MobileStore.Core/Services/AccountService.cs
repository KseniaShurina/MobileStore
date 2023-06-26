using Microsoft.EntityFrameworkCore;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;
using System.Security.Cryptography;
using System.Text;

namespace MobileStore.Core.Services
{
    internal class AccountService : IAccountService
    {
        private const int KeySize = 64;
        private const int Iterations = 350000;
        private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

        private readonly IDefaultContext _context;

        public AccountService(IDefaultContext context)
        {
            _context = context;
        }

        public async Task<UserModel?> GetUserByEmail(string userEmail)
        {
            if (userEmail is null) throw new ArgumentNullException(nameof(userEmail));

            var email = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            return email?.MapToModel();
        }

        public async Task<bool> IsValidPassword(string userEmail, string password)
        {
            if (userEmail == null) throw new ArgumentNullException(nameof(userEmail));

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null) return false;

            var passwordHash = HashPassword(password, user.PasswordSalt);

            var result = passwordHash == user.PasswordHash;
            return result;
        }

        public async Task<UserModel> RegisterUser(UserRegisterModel model)
        {
            if (model == null) throw new ArgumentNullException();

            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null) throw new ArgumentNullException($"{nameof(user)}", "Пользователь с такими параметрами уже существует");

            var salt = Guid.NewGuid().ToString();
            var passwordHash = HashPassword(model.Password, salt);

            user = new User
            {
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.MapToModel();
        }

        private string HashPassword(string password, string salt)
        {
            var saltBytes = Encoding.UTF8.GetBytes(salt);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltBytes,
                Iterations,
                _hashAlgorithm,
                KeySize);
            return Convert.ToHexString(hash);
        }
    }
}
