using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Extensions.Entities
{
    internal static class UserExtensions
    {
        public static UserModel MapToModel(this User entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                Email = entity.Email,
                Address = entity.Address,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };
        }
    }
}
