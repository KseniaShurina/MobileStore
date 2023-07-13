using NUnit.Framework;
using MobileStore.Core.Tests.Helpers.EntityBuilders;
using MobileStore.Core.Services;
using MobileStore.Core.Tests.Helpers;

namespace MobileStore.Core.Tests.Services;

[TestFixture]
[Parallelizable]
public class CartServiceTests : TestFixture
{
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        _cartService = new CartService(DefaultContext);
    }

    [Test]
    public async Task GetCartItems_Expect_Success()
    {
        var res = await _cartService.GetCartItems();

        Assert.That(res, Is.Not.Null);
        Assert.That(res, Is.Empty);

        var productType = await this.CreateProductType("Laptop");
        var product = await this.CreateProduct("MacBook Air", productType.Id);
        await this.CreateCartItem(product.Id, 1, userId: UserId);

        res = await _cartService.GetCartItems();
        Assert.That(res, Is.Not.Null);
        Assert.That(res.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task Create_Expect_Success()
    {
        var productType = await this.CreateProductType("Phone");
        var product1 = await this.CreateProduct("IPhone 12", productType.Id);
        var product2 = await this.CreateProduct("IPhone 13", productType.Id);

        var res = await _cartService.Create(product1.Id, 1);

        Assert.That(res, Is.Not.Null);
        Assert.That(res.Quantity, Is.EqualTo(1));

        var res2 = await _cartService.Create(product1.Id, 1);

        Assert.That(res2, Is.Not.Null);
        Assert.That(res2.Id, Is.EqualTo(res.Id));
        Assert.That(res2.Quantity, Is.EqualTo(2));

        var res3 = await _cartService.Create(product2.Id, 1);

        Assert.That(res3, Is.Not.Null);
        Assert.That(res3.Id, Is.Not.EqualTo(res.Id));
        Assert.That(res3.Quantity, Is.EqualTo(1));
    }

    [TestCase(true, -1)]
    [TestCase(true, 0)]
    [TestCase(true, 1)]
    [TestCase(false, -1)]
    [TestCase(false, 0)]
    public async Task Create_Expect_Exception(bool isEmptyProductId, int quantity)
    {
        //TODO Я не понимаю как это работает
        var productId = (Guid)default;
        if (!isEmptyProductId)
        {
            var productType = await this.CreateProductType("Phone");
            var product = await this.CreateProduct("IPhone 14", productType.Id);
            productId = product.Id;
        }

        //проверка quantity и проверка что продукт не существует
        Assert.ThrowsAsync<ArgumentException>(async () => await _cartService.Create(productId, quantity));
    }

    [TestCase(2)]
    [TestCase(6)]
    [TestCase(10)]
    public async Task UpdateQuantity_Expect_Success(int quantity)
    {
        var productType = await this.CreateProductType("Phone");
        var product = await this.CreateProduct("Z Flip 3", productType.Id);
        var cartItem = await this.CreateCartItem(product.Id, 1);

        var res = await _cartService.UpdateQuantity(cartItem.Id, quantity);

        Assert.That(res, Is.Not.Null);
        Assert.That(res.Id, Is.EqualTo(cartItem.Id));
        Assert.That(res.Quantity, Is.EqualTo(quantity));
    }

    [TestCase(false, true, 0)]
    [TestCase(false, false, -1)]
    [TestCase(false, false, 0)]
    [TestCase(false, false, 1)]
    [TestCase(true, false, -1)]
    [TestCase(true, false, 0)]
    public async Task UpdateQuantity_Expect_Exception(bool isWithCartItemId, bool isDefault, int quantity)
    {
        //TODO Я не понимаю как это работает
        var cartItemId = !isDefault ? Guid.NewGuid() : default;
        if (isWithCartItemId)
        {
            var productType = await this.CreateProductType("Phone");
            var product = await this.CreateProduct("IPhone 16", productType.Id);
            var cartItem = await this.CreateCartItem(product.Id, 1);
            cartItemId = cartItem.Id;
        }

        Assert.ThrowsAsync<ArgumentException>(async () =>
            await _cartService.UpdateQuantity(cartItemId, quantity));

        //if (cartItemId != null)
        //{
        //    //проверка что продукт не существует
        //    Assert.ThrowsAsync<ArgumentNullException>(async () => await _cartService.UpdateQuantity((int)cartItemId, quantity));
        //}
        //проверка quantity
        //Assert.ThrowsAsync<ArgumentException>(async () => await _cartService.UpdateQuantity(cartItemId ?? 1, quantity));
    }

    [Test]
    public async Task Delete_Expect_Success()
    {
        var productType = await this.CreateProductType("Phone");

        var product1 = await this.CreateProduct("IPhone 17", productType.Id);
        var cartItem1 = await this.CreateCartItem(product1.Id, 1);

        await _cartService.Delete(cartItem1.Id);

        var res1 = await _cartService.GetCartItems();
        Assert.That(res1, Is.Empty);

        var product2 = await this.CreateProduct("IPhone 18", productType.Id);
        var cartItem2 = await this.CreateCartItem(product2.Id, 2);
        await _cartService.Delete(cartItem2.Id);
        var res2 = await _cartService.GetCartItems();
        Assert.That(res2, Is.Empty);
    }

    private static readonly IEnumerable<Guid> DeleteExpectExceptionData = new[]
    {
        Guid.Empty,
        Guid.NewGuid(),
    };

    [TestCaseSource(nameof(DeleteExpectExceptionData))]
    public void Delete_Expect_Exception(Guid cartItemId)
    {
        Assert.ThrowsAsync<ArgumentException>(async () => await _cartService.Delete(cartItemId));
    }
}