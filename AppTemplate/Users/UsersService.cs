using AppTemplate.Net8.Database;
using Microsoft.EntityFrameworkCore;

namespace AppTemplate.Net8.Users;

public class UsersService(DataContext dataContext)
{
    public async Task<List<UserModel>> Get()
    {
        var entities = await dataContext.Users.Where(e => e.IsActive)
            .ToListAsync();

        return entities.Select(MapToModel).ToList();
    }

    public async Task<UserModel?> GetById(int id)
    {
        var entity = await dataContext.Users.SingleOrDefaultAsync(e => e.Id == id);
        return entity == null
            ? null
            : MapToModel(entity);
    }

    public async Task<int> Create(UserModel user)
    {
        var entity = new User()
        {
            IsActive = true,
        };
        MapToEntity(user, entity);
        dataContext.Users.Add(entity);

        await dataContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Update(UserModel user)
    {
        var entity = await dataContext.Users.SingleOrDefaultAsync(e => e.Id == user.Id && e.IsActive);
        if (entity == null) return;

        MapToEntity(user, entity);
        await dataContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await dataContext.Users.SingleOrDefaultAsync(e => e.Id == id && e.IsActive);
        if (entity != null)
        {
            entity.IsActive = false;
            await dataContext.SaveChangesAsync();
        }
    }


    private void MapToEntity(UserModel model, User entity)
    {
        entity.Email = model.Email;
        entity.Username = model.Username;
        entity.FirstName = model.FirstName;
        entity.LastName = model.LastName;
    }

    private UserModel MapToModel(User entity)
    {
        return new UserModel()
        {
            Id = entity.Id,
            Email = entity.Email,
            Username = entity.Username,
            FirstName = entity.FirstName,
            LastName = entity.LastName
        };
    }
}