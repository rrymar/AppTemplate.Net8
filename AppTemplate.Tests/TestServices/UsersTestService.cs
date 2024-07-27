using AppTemplate.Tests.Infrastructure;
using AppTemplate.Users;
using RestSharp;

namespace AppTemplate.Tests.TestServices;

public class UsersTestService(RestClient client) 
    : CrudTestService<UserModel>(client)
{
    public override string Url => "/Users";
}