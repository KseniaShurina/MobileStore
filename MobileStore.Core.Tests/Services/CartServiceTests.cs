using NUnit.Framework;
using MobileStore.Core.Tests.Helpers.EntityBuilders;
using MobileStore.Core.Services;
using MobileStore.Core.Tests.Helpers;

namespace MobileStore.Core.Tests.Services
{
    [TestFixture]
    public class CartServiceTests : TestFixture
    {
        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _cartService = new CartService(DefaultContext);
        }

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            await this.CreateUser("test", "test", UserId);
        }

        [Test]
        public async Task GetCartItems_Expect_Success()
        {
            var res = await _cartService.GetCartItems();

            Assert.That(res, Is.Not.Null);
            Assert.That(res, Is.Empty);

            var productType = await this.CreateProductType("Laptop", 2);
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

        [TestCase(null, -1)]
        [TestCase(null, 0)]
        [TestCase(null, 1)]
        [TestCase(1, -1)]
        [TestCase(1, 0)]
        public async Task Create_Expect_Exception(int? productId, int quantity)
        {
            //TODO Я не понимаю как это работает
            if (productId != null)
            {
                var productType = await this.CreateProductType("Phone");
                await this.CreateProduct("IPhone 14", productType.Id, id: productId);
            }
            //проверка quantity и проверка что продукт не существует
            Assert.ThrowsAsync<ArgumentException>(async () => await _cartService.Create(productId ?? 0, quantity));
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

        [TestCase(null, -1)]
        [TestCase(null, 0)]
        [TestCase(null, 1)]
        [TestCase(1, -1)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        public async Task UpdateQuantity_Expect_Exception(int? cartItemId, int quantity)
        {
            //TODO Я не понимаю как это работает
            if (cartItemId != null)
            {
                var productType = await this.CreateProductType("Phone");
                await this.CreateProduct("IPhone 16", productType.Id, id: cartItemId);
            }
            Assert.ThrowsAsync<ArgumentException>(async () => await _cartService.UpdateQuantity(cartItemId ?? 0, quantity));

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

            var product2 = await this.CreateProduct("IPhone 18", productType.Id, 2);
            var cartItem2 = await this.CreateCartItem(product2.Id, 2);
            await _cartService.Delete(cartItem2.Id);
            var res2 = await _cartService.GetCartItems();
            Assert.That(res2, Is.Empty);
        }

        [TestCase(3)]
        [TestCase(-1)]
        [TestCase(10)]
        public void Delete_Expect_Exception(int cartItemId)
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _cartService.Delete(cartItemId));
        }
    }
}
