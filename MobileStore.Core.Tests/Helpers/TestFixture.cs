using Microsoft.EntityFrameworkCore;
using MobileStore.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MobileStore.Common.Identity;
using MobileStore.Core.Tests.Helpers.EntityBuilders;
using NUnit.Framework;

namespace MobileStore.Core.Tests.Helpers;

public class TestFixture : IDisposable
{
    private bool _disposed;

    internal Guid UserId => IdentityState.Current!.UserId;

    internal DefaultContext DefaultContext { get; }

    [SetUp]
    public virtual void SetUp()
    {
        var id = Guid.NewGuid();
        IdentityState.SetCurrent(id);
        this.CreateUser($"test_{id}", "test", UserId);
    }

    public TestFixture()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;


        
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