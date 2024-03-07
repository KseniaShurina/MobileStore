using Microsoft.EntityFrameworkCore;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Infrastructure.Abstractions.Contexts;
using Npgsql;
using Ardalis.GuardClauses;
using MobileStore.Core.Models;

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
            var content = await _context.Contents.FirstOrDefaultAsync(i => i.Id == contentId);

            if (content == null)
            {
                throw new ArgumentException($"Content does not exist {nameof(contentId)}");
            }

            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// This method accepts a collection of contentIds of the content to be removed from the database.
        /// </summary>
        /// <param name="contentIds">collection of identifiers</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task Delete(IEnumerable<Guid> contentIds)
        {
            // Converts the contentIds parameter to a list
            contentIds = contentIds.ToList();

            // Retrieves content from the database by id
            var contents = await _context.Contents
                .Where(i => contentIds.Contains(i.Id))
                .ToListAsync();;

            // Checks whether the number of elements in contents matches the number of elements in the original contentIds collection.
            if (contentIds.Count() != contents.Count)
            {
                throw new ArgumentException
                    ($"The number of elements does not match the number of elements of the current collection. Error occurred in Delete");
            }

            // Removes found content from the database context
            _context.Contents.RemoveRange(contents);
            await _context.SaveChangesAsync();
        }
    }
}
