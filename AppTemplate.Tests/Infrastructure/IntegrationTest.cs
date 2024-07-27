using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Tests.Infrastructure;

[AutoRollback]
public abstract class IntegrationTest<TStartup,TDbContext>
    where TDbContext : DbContext
    where TStartup : class
{
    private readonly TestApplicationFactoryBase<TStartup,TDbContext> _factory;

    protected readonly TDbContext DataContext;

    protected readonly IServiceProvider Services;

    private readonly IServiceScope _scope;

    protected IntegrationTest(TestApplicationFactoryBase<TStartup, TDbContext> factory)
    {
        _factory = factory;
        factory.Server.PreserveExecutionContext = true;

        _scope = factory.Services.CreateScope();
        Services = _scope.ServiceProvider;

        DataContext = _scope.ServiceProvider.GetRequiredService<TDbContext>();

    }

    public virtual void Dispose()
    {
        _scope?.Dispose();
    }
}