using fStore.Core;
using Microsoft.EntityFrameworkCore;

namespace fStore.WEBAPI;

public class UserRepo : BaseRepo<User>, IUserRepo
{
    public UserRepo(DataBaseContext dataBaseContext) : base(dataBaseContext)
    {
    }

    public override async Task<IEnumerable<User>> GetAllAsync(GetAllParams options)
    {
        var users = await _data.AsNoTracking().Where(u => u.Name.Contains(options.Search)).Skip(options.Offset).Take(options.Limit).ToListAsync();// Task.WhenAll(Task.Run(()=> _users.Skip(options.Offset).Take(options.Limit)));
        return users;
    }

    public Task<User?> FindByEmailAsync(string email)
    {
        return _data.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public override async Task<User> UpdateOneAsync(Guid id, User updateObject)
    {
        var user = await _data.FindAsync(id);
        if (user is null)
        {
            return null;
        }
        if (updateObject.Name is not null)
        {
            user.Name = updateObject.Name;
        }
        if (updateObject.Avatar is not null)
        {
            user.Avatar = updateObject.Avatar;
        }
        if (updateObject.Role != user.Role)
        {
            user.Role = updateObject.Role;
        }
        if (updateObject.Email is not null && user.Email != updateObject.Email)
        {
            user.Email = updateObject.Email;
        }
        if (updateObject.Password is not null && updateObject.Password != user.Password)
        {
            user.Password = updateObject.Password;
        }
        if (updateObject.Salt is not null && updateObject.Salt != user.Salt)
        {
            user.Salt = updateObject.Salt;
        }
        _data.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> IsEmailAvailable(string email)
    {
        var result = await _data.FindAsync(email);
        if (result is null)
        {
            return false;
        }
        return true;
    }
}
