using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace MobileStore.Infrastructure.Abstractions.Contexts.Base;
// Contain methods for interaction with 
public interface IDbContextBase : IDisposable, IAsyncDisposable
{
    DbConnection GetDbConnection();

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    EntityEntry Entry(object entity);

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        where TEntity : class;

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);
}