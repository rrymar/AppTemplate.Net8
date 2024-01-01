using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Net8.Tests;

public class TestApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
  //  where TDbContext : DbContext
    where TStartup : class
{
    // public virtual List<ITestMigration<TDbContext>> TestMigrations
    //     => new List<ITestMigration<TDbContext>>();

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
            // var provider = s.BuildServiceProvider();
            // var db = provider.GetRequiredService<TDbContext>();
            // db.InitTestDatabases(typeof(TMigrationScripts).Assembly, TestMigrations);
            // provider.Dispose();

            ConfigureTestServices(s);
        });
    }

    protected virtual void ConfigureTestServices(IServiceCollection services)
    {
      //  services.AddScoped(_ => new RestClient(CreateTestClient()));
    }
}