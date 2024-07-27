namespace AppTemplate.Net8.Users;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<UsersService>();
        return services;
    }
}