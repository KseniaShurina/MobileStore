using MobileStore.Core.Helpers;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Tests.Helpers.EntityBuilders;

internal static partial class TestEntityBuilder
{
    internal static User CreateUser(this TestFixture test,
        string email, string password, Guid? id = null)
    {
        var entity = CreateUserEntity(email, password, id);

        test.DefaultContext.Users.Add(entity);
        test.DefaultContext.SaveChanges();

        return entity;
    }

    internal static async Task<User> CreateUserAsync(this TestFixture test,
        string email, string password, Guid? id = null)
    {
        var entity = CreateUserEntity(email, password, id);

        await test.DefaultContext.Users.AddAsync(entity);
        await test.DefaultContext.SaveChangesAsync();

        return entity;
    }

    private static User CreateUserEntity(string email, string password, Guid? id = null)
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

        return entity;
    }
}