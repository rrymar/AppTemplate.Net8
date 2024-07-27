using Microsoft.AspNetCore.Mvc;

namespace AppTemplate.Users;

[ApiController]
[Route("[controller]")]
public class UsersController(UsersService usersService) : Controller
{
    [HttpGet]
    public async Task<List<UserModel>> Get()
    {
        return await usersService.Get();
    } 
    
    [HttpGet("{id}")]
    public async Task<UserModel?> GetById(int id)
    {
        return await usersService.GetById(id);
    } 
    
    [HttpPost]
    public async Task<UserModel?> Create(UserModel user)
    {
        var id = await usersService.Create(user);
        return await usersService.GetById(id);
    } 
    
    [HttpPut]
    public async Task<UserModel?> Update(UserModel user)
    {
        await usersService.Update(user);
        return await usersService.GetById(user.Id);
    } 
    
    [HttpDelete]
    public async Task Delete(int id)
    {
        await usersService.Delete(id);
    } 
}