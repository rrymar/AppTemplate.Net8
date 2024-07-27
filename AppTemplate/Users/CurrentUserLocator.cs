using AppTemplate.Database;

namespace AppTemplate.Users;

public class CurrentUserLocator : ICurrentUserLocator
{
    public int UserId => KnownUsers.System;
}