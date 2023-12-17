using fStore.Core;
using Microsoft.EntityFrameworkCore;

namespace fStore.WEBAPI;

public class UserRepo : IUserRepo
{
    private DbSet<User> _users;
    private DataBaseContext _dbContext;

    public UserRepo(DataBaseContext context)
    {
        _users = context.Users;
        _dbContext = context;
        // if this is external database (postgresql) -> only make a query
    }

    public async Task<User> CreateOneAsync(User createObject)
    {
        await _users.AddAsync(createObject);
        await _dbContext.SaveChangesAsync();
        return createObject;
    }

    public async Task<bool> DeleteByIdAsync(User deleteObject)
    {
        _users.Remove(deleteObject);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<User>> GetAllAsync(GetAllParams options)
    {
        var users = await _users.AsNoTracking().Where(u=> u.Name.Contains(options.Search)).Skip(options.Offset).Take(options.Limit).ToListAsync();// Task.WhenAll(Task.Run(()=> _users.Skip(options.Offset).Take(options.Limit)));
        return users;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<User?> FindByEmailAsync(string email)
    {
        return _users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> UpdateOneAsync(Guid id, User updateObject)
    {
        var user = await _users.FindAsync(id);
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
        if (updateObject.Password is not null && updateObject.Password != user.Password)
        {
            user.Password= updateObject.Password;
        }
        if (updateObject.Salt is not null && updateObject.Salt != user.Salt)
        {
            user.Salt = updateObject.Salt;
        }
        _users.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
}
