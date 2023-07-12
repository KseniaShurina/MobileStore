using MobileStore.Core.Models;
using MobileStore.Core.Services;
using MobileStore.Core.Tests.Helpers;
using MobileStore.Core.Tests.Helpers.EntityBuilders;
using NUnit.Framework;

namespace MobileStore.Core.Tests.Services;

[TestFixture]
public class AccountServiceTests : TestFixture
{
    private readonly AccountService _accountService;

    public AccountServiceTests()
    {
        _accountService = new AccountService(DefaultContext);
    }

    [TearDown]
    public void TearDown()
    {
        DefaultContext.RemoveRange(DefaultContext.Users.ToList());
    }

    [TestCase("test-get-by-email@mail.com")]
    public async Task GetUserByEmail_Expect_Success(string email)
    {
        // создаем фейк юзера и доб его в фейк дб
        await this.CreateUser(email, Guid.NewGuid().ToString());
        // достаём фейк юзера из фейк дб
        var res = await _accountService.GetUserByEmail(email);
        //проверка что юзер не null. Если null то юнит завершит тест и покажет ошибку
        Assert.That(res, Is.Not.Null);
        //тут проверяется валидность. Емаил равен тому емаилу, что мы туда положили при создании юзера
        Assert.That(res!.Email, Is.EqualTo(email));
    }

    [Test]
    public void GetUserByEmail_Expect_Exception()
    {
        Assert.ThrowsAsync<ArgumentNullException>(async () => await _accountService.GetUserByEmail(null!));
        Assert.ThrowsAsync<ArgumentNullException>(async () => await _accountService.GetUserByEmail(""));
    }

    [TestCase("test-is-valid-pass_true@mail.com", "123")]
    public async Task IsValidPassword_Expect_True(string email, string password)
    {
        await this.CreateUser(email, password);

        var res = await _accountService.IsValidPassword(email, password);
        Assert.That(res, Is.True);

    }

    [TestCase(null, null)]
    [TestCase("", "")]
    [TestCase("test-is-valid-pass_false@mail.com", null)]
    [TestCase("test-is-valid-pass_false@mail.com", "")]
    [TestCase("test-is-valid-pass_false@mail.com", "123")]
    public async Task IsValidPassword_Expect_False(string email, string password)
    {
        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            await this.CreateUser(email, password);
        }
        
        var res = await _accountService.IsValidPassword(email, Guid.NewGuid().ToString());
        Assert.That(res, Is.False);

        res = await _accountService.IsValidPassword(Guid.NewGuid().ToString(), password);
        Assert.That(res, Is.False);

        res = await _accountService.IsValidPassword(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        Assert.That(res, Is.False);
    }

    [TestCase("test-register@mail.com", "123")]
    public async Task RegisterUser_Expect_Success(string email, string password)
    {
        var model = new UserRegisterModel(email, password);
        var res = await _accountService.RegisterUser(model);

        Assert.That(res, Is.Not.Null);
        Assert.That(res.Email!.ToLower(), Is.EqualTo(email.ToLower()));
    }

    [TestCase(null, null, typeof(ArgumentNullException))]
    [TestCase("", "", typeof(ArgumentException))]
    [TestCase(" ", " ", typeof(ArgumentException))]
    [TestCase("Test-Register_exception@mail.com", "123", typeof(ArgumentException))]
    public async Task RegisterUser_Expect_Exception(string email, string password, Type exceptionType)
    {
        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            await this.CreateUser(email, password);
        }

        Assert.ThrowsAsync<ArgumentNullException>(async () => await _accountService.RegisterUser(null!));
        Assert.ThrowsAsync(exceptionType, async () => await _accountService.RegisterUser(new UserRegisterModel(email, password)));
    }
}