using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileStore.Common.Identity;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Services
{
    internal class UserService : IUserService
    {
        public readonly IDefaultContext _context;

        public UserService(IDefaultContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            return IdentityState.Current!.UserId;
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
