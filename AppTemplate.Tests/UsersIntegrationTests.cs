using AppTemplate.Database;
using AppTemplate.Tests.Infrastructure;
using AppTemplate.Users;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace AppTemplate.Tests;

public class UsersIntegrationTests : AppTemplateIntegrationTests
{
    public UsersIntegrationTests(TestApplicationFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public void Test1()
    {
        var restClient = Services.GetRequiredService<RestClient>();
        var result = restClient.Get<List<UserModel>>("/Users");
        result.Should().NotBeEmpty();
    }
}