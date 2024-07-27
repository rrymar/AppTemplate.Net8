using AppTemplate.Net8.Database;

namespace AppTemplate.Net8.Users;

public class CurrentUserLocator : ICurrentUserLocator
{
    public int UserId => KnownUsers.System;
}