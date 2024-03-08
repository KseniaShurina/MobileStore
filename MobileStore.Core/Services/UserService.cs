using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using MobileStore.Common.Abstractions.Services;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Abstractions.Contexts;

namespace MobileStore.Core.Services
{
    internal class UserService : IUserService
    {
        private readonly IDefaultContext _context;
        private readonly IReadIdentityService _identityService;

        public UserService(IDefaultContext context, IReadIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        private Guid GetUserId()
        {
            return _identityService.UserId!.Value;
        }

        public async Task<UserModel> GetCurrentUser()
        {
            var userId = GetUserId();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new NullReferenceException(nameof(user));
            }
            else
            {
                return user.MapToModel();
            }
        }

        public async Task UpdateCurrentUser(UserModel model)
        {
            Guard.Against.Null(model);
            try
            {
                var userId = GetUserId();
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null) throw new ArgumentException(nameof(user));

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            await _context.SaveChangesAsync();
        }

        //private IEnumerable<string> PasswordStrenght(string password)
        //{
        //    if (string.IsNullOrWhiteSpace(password))
        //    {
        //        yield return "Password is required";
        //        yield break;
        //    }

        //    if (password.Length < 8)
        //    {
        //        yield return "password must be at least of length 8";
        //    }

        //    if (!Regex.IsMatch(password, @"[a-z]"))
        //    {
        //        yield return "password must contain at least one lowercase letter";
        //    }

        //    if (!Regex.IsMatch(password, @"[0-9]"))
        //    {
        //        yield return "password must contain at least one digit";
        //    }
        //}
    }
}
