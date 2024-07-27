using AppTemplate.Tests.Infrastructure;
using AppTemplate.Tests.TestServices;
using AppTemplate.Users;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Tests;

public class UsersIntegrationTests : AppTemplateIntegrationTests
{
    private readonly UsersTestService _usersTestService;
    public UsersIntegrationTests(TestApplicationFactory factory)
        : base(factory)
    {
        _usersTestService = Services.GetRequiredService<UsersTestService>();
    }

    [Fact]
    public void ItCreatesUser()
    {
        var toCreate = new UserModel()
        {
            FirstName = "Test First",
            LastName = "Test Last",
            Email = "some@email.com",
            Username = "username1"
        };

        var created = _usersTestService.Create(toCreate);
        created.Username.Should().Be(toCreate.Username);
        created.FirstName.Should().Be(toCreate.FirstName);
        created.LastName.Should().Be(toCreate.LastName);

        created.Id.Should().NotBe(0);
    }

    [Fact]
    public void ItReturnsUser()
    {
        var user = _usersTestService.Get(1);
        user.Should().NotBeNull();
    }
}