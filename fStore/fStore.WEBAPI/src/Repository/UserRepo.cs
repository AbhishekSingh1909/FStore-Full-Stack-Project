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
    public User CreateUser(User user)
    {
        _users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }

    public IEnumerable<User> GetAllUsers(GetAllParams options)
    {
        return _users.Where(u => u.Name.Contains(options.Search)).Skip(options.Offset).Take(options.Limit);
    }

    public User GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }
}
