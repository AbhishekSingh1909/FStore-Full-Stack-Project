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
        var users = await _data.AsNoTracking().Include(u => u.Address).Where(u => u.Name.Contains(options.Search)).Skip(options.Offset).Take(options.Limit).ToListAsync();// Task.WhenAll(Task.Run(()=> _users.Skip(options.Offset).Take(options.Limit)));
        return users;
    }

    public override async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await _data.AsNoTracking().Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public Task<User?> FindByEmailAsync(string email)
    {
        return _data.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public override async Task<User> UpdateOneAsync(Guid id, User updateObject)
    {

        _data.Update(updateObject);
        await _dbContext.SaveChangesAsync();
        return updateObject;
    }

    public async Task<bool> IsEmailAvailableAsync(string email)
    {
        var result = await _data.FindAsync(email);
        if (result is null)
        {
            return false;
        }
        return true;
    }
}
