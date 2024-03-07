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

        /// <summary>
        /// Retrieves a user from the database based on the provided email
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The UserModel, or null if no user is found.</returns>
        public async Task<UserModel?> GetUserByEmail(string email)
        {
            Guard.Against.NullOrEmpty(email, nameof(email));

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());
            return user?.MapToModel();
        }

        /// <summary>
        /// Checks if the password matches the password associated with the specified email.
        /// </summary>
        /// <param name="email">The email associated with the user.</param>
        /// <param name="password">The password to validate.</param>
        /// <returns>True if the password is valid for the specified email, false otherwise.</returns>
        public async Task<bool> IsValidPassword(string email, string password)
        {
            if (string.IsNullOrEmpty(email)) return false;
            if (string.IsNullOrEmpty(password)) return false;

            // User Retrieval
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());

            if (user == null) return false;

            //If a user is found, the method hash the provided password associated with the user retrieved from the database.
            var passwordHash = PasswordHelper.HashPassword(password, user.PasswordSalt);

            //Compares the hashed password with the hashed password stored in the database.
            var result = passwordHash == user.PasswordHash;
            return result;
        }

        /// <summary>
        /// Registers a new user based on the provided user registration model.
        /// </summary>
        /// <param name="model">The user registration model containing the user's information.</param>
        /// <returns>The UserModel representing the newly registered user.</returns>
        /// <exception cref="ArgumentException">Thrown if a user with the same email already exists.</exception>
        public async Task<UserModel> RegisterUser(UserRegisterModel model)
        {
            Guard.Against.Null(model);

            // The email from the model is normalized to lowercase
            var email = model.Email.ToLower();

            // It checks if a user with the provided email already exists in the database.
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

            if (user != null) throw new ArgumentException("User with these parameters already exist", $"{nameof(user)}");

            // A new password salt is generated using
            var salt = Guid.NewGuid().ToString();
            // The password from the model is hashed to securely store
            var passwordHash = PasswordHelper.HashPassword(model.Password, salt);
            // A new User entity is created with the provided email, hashed password, and generated salt.
            user = new User
            {
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            // Returning User Model
            return user.MapToModel();
        }
    }
}
