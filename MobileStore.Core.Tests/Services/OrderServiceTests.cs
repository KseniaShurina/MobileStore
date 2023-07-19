﻿using MobileStore.Core.Models;
using MobileStore.Core.Services;
using MobileStore.Core.Tests.Helpers;
using MobileStore.Core.Tests.Helpers.EntityBuilders;
using NUnit.Framework;

namespace MobileStore.Core.Tests.Services
{
    [TestFixture]
    [Parallelizable]
    public class OrderServiceTests : TestFixture
    {
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderService = new OrderService(DefaultContext);
        }

        [Test]
        public async Task CreateOrder_Expect_Success()
        {
            var productType = await this.CreateProductType("Watches");
            var product1 = await this.CreateProduct("Apple Watch Series 8", productType.Id);
            var product2 = await this.CreateProduct("Apple Watch Series 9", productType.Id);
            var cartItem1 = await this.CreateCartItem(product1.Id, 1);
            var cartItem2 = await this.CreateCartItem(product2.Id, 1);

            var orderCreateModel = new OrderCreateModel
            {
                Email = "Email",
                FirstName = "FirstNameTest",
                LastName = "LastNameTest",
                ContactPhone = "+375-29-123-45-67",
                Address = "Address",
            };

            var result = await _orderService.CreateOrder(orderCreateModel);

            Assert.That(result, Is.Not.Null);
            //TODO Add email to entity and model
            //Assert.That(result.Email, Is.EqualTo(orderCreateModel.Email));
            //Assert.That(result.FirstName, Is.EqualTo(orderCreateModel.FirstName));
            Assert.That(result.Address, Is.EqualTo(orderCreateModel.Address));

        }

        private static readonly IEnumerable<OrderCreateModel> CreateOrderExpectExceptionData = new[]
        {
            new OrderCreateModel
            {
                Email = "ooo2000@gmail.com",
                FirstName = "Oleg",
                LastName = "",
                ContactPhone = "+375-29-123-45-67",
                Address = "Address",
            },

            new OrderCreateModel
            {
                Email = "",
                FirstName = "Kate",
                LastName = "Smite",
                ContactPhone = "+375-29-395-43-19",
                Address = "ul. Pupcina 7d",
            },
        };

        [TestCaseSource(nameof(CreateOrderExpectExceptionData))]
        public void CreateOrder_Expect_Exception(OrderCreateModel orderCreateModel)
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await _orderService.CreateOrder(orderCreateModel));
        }

        //[Test]
        //public async Task GetOrderItems_Expect_Success()
        //{
        //    var productType = await this.CreateProductType("Phones");
        //    var product1 = await this.CreateProduct("iPhone 12 Pro", productType.Id);
        //    var product2 = await this.CreateProduct("iPhone 12 mini", productType.Id);
        //    var cartItem1 = await this.CreateCartItem(product1.Id, 1);
        //    var cartItem2 = await this.CreateCartItem(product2.Id, 1);
        //    var orderItem1 = await this.CreateOrderItem(cartItem1.Quantity, cartItem1.ProductId);
        //    var orderItem2 = await this.CreateOrderItem(cartItem2.Quantity, cartItem2.ProductId);

        //    var result = await _orderService.GetOrderItems();

        //    Assert.That(result, Is.Not.Null);
        //    Assert.That(result, Is.Not.Null);
        //}
    }
}
