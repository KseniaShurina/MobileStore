using Microsoft.EntityFrameworkCore;
using MobileStore.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MobileStore.Common.Identity;

namespace MobileStore.Core.Tests.Helpers;

public class TestFixture : IDisposable
{
    private bool _disposed;

    private readonly int _userId = 1;

    internal int UserId => IdentityState.Current!.UserId;

    internal DefaultContext DefaultContext { get; }

    public TestFixture()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;


        IdentityState.SetCurrent(_userId);

        DefaultContext = new DefaultContext(options);
        DefaultContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            DefaultContext.Dispose();
        }

        _disposed = true;
    }
}