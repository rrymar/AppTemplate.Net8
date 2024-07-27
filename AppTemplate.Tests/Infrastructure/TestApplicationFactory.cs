using System.Reflection;
using AppTemplate.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace AppTemplate.Tests.Infrastructure;

public class TestApplicationFactory : TestApplicationFactoryBase<Program, DataContext>;

public abstract class TestApplicationFactoryBase<TStartup,TDbContext> : WebApplicationFactory<TStartup>
    where TDbContext : DbContext
    where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseEnvironment("Tests");
        builder.ConfigureTestServices(ConfigureTestServices);
    }

    protected virtual void ConfigureTestServices(IServiceCollection services)
    {
        services.AddScoped(_ => new RestClient(CreateClient()));
        
        var serviceTypes = GetType().Assembly.GetTypes()
            .Where(t => t.Name.EndsWith("TestService")).ToList();
        serviceTypes.ForEach(s => services.AddScoped(s));
    }
}