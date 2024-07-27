using AppTemplate.Database;
using AppTemplate.Tests.Infrastructure;
using AppTemplate.Users;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace AppTemplate.Tests;

[Collection(FixtureCollection.Name)]
public class AppTemplateIntegrationTests : IntegrationTest<Program, DataContext>
{
    public AppTemplateIntegrationTests(TestApplicationFactory factory)
        : base(factory)
    {
    }
}