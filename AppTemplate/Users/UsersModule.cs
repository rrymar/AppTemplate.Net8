using AppTemplate.Database;

namespace AppTemplate.Users;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<UsersService>();
        services.AddTransient<ICurrentUserLocator, CurrentUserLocator>();
        return services;
    }
}