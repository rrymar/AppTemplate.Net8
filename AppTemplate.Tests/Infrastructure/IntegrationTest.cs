using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Tests.Infrastructure;

[AutoRollback]
public abstract class IntegrationTest<TDbContex, TStartup>
    where TDbContex : DbContext
    where TStartup : class
{
    private readonly TestApplicationFactory<TStartup,TDbContex> _factory;

    protected virtual bool CreateTransaction => false;

    protected readonly TDbContex DataContext;

    protected readonly IServiceProvider Services;

    private readonly IServiceScope _scope;

    protected IntegrationTest(TestApplicationFactory<TStartup, TDbContex> factory)
    {
        _factory = factory;
        factory.Server.PreserveExecutionContext = true;

        _scope = factory.Services.CreateScope();
        Services = _scope.ServiceProvider;

        DataContext = _scope.ServiceProvider.GetRequiredService<TDbContex>();

    }

    public virtual void Dispose()
    {
        _scope?.Dispose();
    }
}