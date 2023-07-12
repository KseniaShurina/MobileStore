using MobileStore.Core.Helpers;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Tests.Helpers.EntityBuilders;

internal static partial class TestEntityBuilder
{
    internal static async Task<User> CreateUser(this TestFixture test,
        string email, string password, int? id = null)
    {
        var passwordSalt = Guid.NewGuid().ToString();
        var passwordHash = PasswordHelper.HashPassword(password, passwordSalt);

        var entity = new User
        {
            Email = email.ToLower(),
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };

        if (id != null)
        {
            entity.Id = id.Value;
        }

        await test.DefaultContext.Users.AddAsync(entity);
        await test.DefaultContext.SaveChangesAsync();

        return entity;
    }
}