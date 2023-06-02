using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MobileStore.Infrastructure.Abstractions.Contexts.Base;

public interface IDbContextBase
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    EntityEntry Entry(object entity);

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        where TEntity : class;
}