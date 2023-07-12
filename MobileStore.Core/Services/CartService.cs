﻿using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using MobileStore.Common.Identity;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Services
{
    internal class CartService : ICartService
    {
        private readonly IDefaultContext _context;

        public CartService(IDefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IQueryable<CartItem> GetBaseQuery()
        {
            return _context.CartItems
                .AsNoTracking()
                .Include(i => i.Product);
        }

        /// <summary>
        /// Got cart item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Task<CartItem?> Get(int id)
        {
            return GetBaseQuery()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ProductTypeModel>> GetProductTypes()
        {
            var entities = await _context.ProductTypes
                .AsNoTracking().ToListAsync();
            return entities
                .Select(MapFromEntity)
                .ToList();
        }

        public async Task<List<CartItemModel>> GetCartItems()
        {
            var userId = IdentityState.Current!.UserId;
            var entity = await GetBaseQuery()
                .Where(i => i.UserId == userId)
                .ToListAsync();

            return entity.Select(i => i.MapToModel()).ToList();
        }

        /// <summary>
        /// Convert product to CartItem
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<CartItemModel> Create(int productId, int quantity)
        {
            quantity = Guard.Against.NegativeOrZero(quantity);
            productId = Guard.Against.NegativeOrZero(productId);

            var userId = IdentityState.Current!.UserId;

            if  (!await _context.Products.AnyAsync(p => p.Id == productId))
            {
                throw new ArgumentException($"Product id = {productId} not found", nameof(productId));
            }

            var item = await _context.CartItems.FirstOrDefaultAsync(p => p.ProductId == productId);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                item = new CartItem
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = quantity,
                };
                await _context.CartItems.AddAsync(item);
            }
            await _context.SaveChangesAsync();

            return (await Get(item.Id))!.MapToModel();
        }

        public async Task<CartItemModel> UpdateQuantity(int cartItemId, int quantity)
        {
            cartItemId = Guard.Against.NegativeOrZero(cartItemId);
            quantity = Guard.Against.NegativeOrZero(quantity);

            var item = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

            if (item == null)
            {
                throw new ArgumentException($"CartItem id = {cartItemId} not found", nameof(cartItemId));
            }

            item.Quantity = quantity;
            await _context.SaveChangesAsync();

            return (await Get(item.Id))!.MapToModel();
        }

        public async Task Delete(int cartItemId)
        {
            var item = await _context.CartItems
                .Where(i => i.Id == cartItemId)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                throw new ArgumentNullException($"Product not found {nameof(item)}");
            }
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }


        private static ProductTypeModel MapFromEntity(ProductType entity)
        {
            return new ProductTypeModel
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
