using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Tests.Infrastructure;

public class TestApplicationFactory<TStartup,TDbContext> : WebApplicationFactory<TStartup>
    where TDbContext : DbContext
    where TStartup : class
{
    // public IHttpClient CreateTestClient()
    // {
    //     return new TestingHttpClient(CreateClient());
    // }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

       // var projectDir = Directory.GetCurrentDirectory();
       // var configPath = Path.Combine(projectDir, "appsettings.json");

       //builder.ConfigureAppConfiguration((_, c) => c.AddJsonFile(configPath));

        builder.ConfigureTestServices(s =>
        {
            ConfigureTestServices(s);
        });
    }

    protected virtual void ConfigureTestServices(IServiceCollection services)
    {
    }
}