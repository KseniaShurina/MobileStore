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
    }
}
