using Microsoft.EntityFrameworkCore;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Infrastructure.Abstractions.Contexts;
using Npgsql;
using Ardalis.GuardClauses;
using MobileStore.Core.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MobileStore.Core.Services
{
    internal class ContentService : IContentService
    {
        private readonly IDefaultContext _context;
        public ContentService(IDefaultContext context)
        {
            _context = context;
        }

        public async Task<ContentInfoModel> SaveFileToDatabase(
            string contentType, string name, Stream stream, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(contentType);
            Guard.Against.NullOrEmpty(name);
            Guard.Against.Null(stream);

            var id = Guid.NewGuid();

            stream.Position = 0;

            NpgsqlConnection? dbConnection = null;

            try
            {
                // Open a connection to the database.
                await using (dbConnection = _context.GetDbConnection() as NpgsqlConnection)
                {
                    //await dbConnection!.OpenAsync();
                    await dbConnection!.OpenAsync(cancellationToken);

                    // Open a large object transaction.
                    await using var transaction = await dbConnection.BeginTransactionAsync(cancellationToken);
                    // It's query
                    var sql =
                        "INSERT INTO public.\"Contents\"(\"Id\", \"ContentType\", \"Name\", \"Data\")" +
                        "VALUES (@id, @contentType, @name, @data)";

                    //command of SQL is being created
                    await using (var command = new NpgsqlCommand(sql, dbConnection))
                    {
                        command.Parameters.Add(
                            new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Uuid) { Value = id });

                        command.Parameters.Add(
                            new NpgsqlParameter("@contentType", NpgsqlTypes.NpgsqlDbType.Text)
                                { Value = contentType });

                        command.Parameters.Add(                                                                                             
                            new NpgsqlParameter("@name", NpgsqlTypes.NpgsqlDbType.Text) { Value = name });

                        // Add stream data as a parameter to the command
                        // передаем данные в команду через параметры
                        command.Parameters.Add(
                            new NpgsqlParameter("@data", NpgsqlTypes.NpgsqlDbType.Bytea) { Value = stream });

                        // Execute the command
                        await command.ExecuteNonQueryAsync(cancellationToken);
                    }

                    await transaction.CommitAsync(cancellationToken);

                    return (await GetContentInfo(id, cancellationToken))!;
                }
            }
            catch
            {
                if (dbConnection != null)
                {
                    await dbConnection.CloseAsync();
                }

                throw;
            }
        }

        public async Task<ContentInfoModel?> GetContentInfo(
            Guid contentId, CancellationToken cancellationToken)
        {
            var content = await _context.Contents
                .Where(i => i.Id == contentId)
                .Select(i => new ContentInfoModel
                {
                    Id = i.Id,
                    ContentType = i.ContentType,
                    Name = i.Name,
                })
                .FirstOrDefaultAsync(cancellationToken);

            return content;
        }

        public async Task<Stream> Get(Guid contentId, CancellationToken cancellationToken)
        {
            var content = await _context.Contents
                              .AsNoTracking()
                              .FirstOrDefaultAsync(i => i.Id == contentId, cancellationToken: cancellationToken) ??
                          throw new ArgumentNullException($"Content does not exist {nameof(contentId)}");

            var stream = new MemoryStream(content.Data);

            return stream;
        }

        public async Task Delete(Guid contentId)
        {
            var content = await _context.Contents.FirstOrDefaultAsync(i => i.Id == contentId) ?? 
                          throw new ArgumentNullException($"Content does not exist {nameof(contentId)}");

            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();
        }

        #region MyRegion

        //public async Task<ProductModel> SaveFileToDatabase(Guid productTypeId, string productTypeName, string name, string company, double price, string img)
        //{
        //    var productTypeExist = await _context.Products.AnyAsync(p => p.ProductTypeId == productTypeId);
        //    Product? product = null;
        //    if (productTypeExist)
        //    {
        //        product = new Product
        //        {
        //            Id = Guid.NewGuid(),
        //            ProductTypeId = productTypeId,
        //            Name = name,
        //            Company = company,
        //            Price = price,
        //            Img = img
        //        };
        //    }
        //    else
        //    {
        //        var productType = new ProductType
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = productTypeName,
        //        };

        //        product = new Product
        //        {
        //            Id = Guid.NewGuid(),
        //            ProductTypeId = productType.Id,
        //            Name = name,
        //            Company = company,
        //            Price = price,
        //            Img = img
        //        };
        //    }

        //    await _context.Products.AddAsync(product);
        //    await _context.SaveChangesAsync();
        //    return product.MapToModel();
        //    //throw new NotImplementedException();
        //}

        //public async Task<ProductModel> Update(ProductModel productModel)
        //{
        //    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productModel.Id);
        //    if (product == null)
        //    {
        //        throw new ArgumentNullException($"Product does not exist {nameof(product.Id)}");
        //    }

        //    var updatedProduct = product.MapToModel();

        //    productModel = updatedProduct;

        //    await _context.SaveChangesAsync();

        //    return updatedProduct;
        //}

        //public async Task Delete(Guid productId)
        //{
        //    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        //    if (product == null)
        //    {
        //        throw new ArgumentException($"Product not found {nameof(product)}");
        //    }
        //    _context.Products.Remove(product);
        //    await _context.SaveChangesAsync();
        //}

        #endregion
    }
}
