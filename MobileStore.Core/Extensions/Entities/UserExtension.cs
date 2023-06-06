using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Extensions.Entities
{
    internal static class UserExtension
    {
        public static UserModel MapToModel(this User entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                Email = entity.Email,
            };
        }
    }
}
