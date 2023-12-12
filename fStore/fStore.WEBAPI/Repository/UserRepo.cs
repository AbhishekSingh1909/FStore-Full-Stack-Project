using fStore.Core;
using Microsoft.EntityFrameworkCore;

namespace fStore.WEBAPI;

public class UserRepo : IUserRepo
{
    private DbSet<User> _users;

    public UserRepo(DataBaseContext context)
    {
        _users = context.Users;
        
    }
    public User CreateUser(User user)
    {
        _users.Add(user);
        return user;
    }

    public IEnumerable<User> GetAllUsers(GetAllParams options)
    {
        throw new NotImplementedException();
    }

    public User GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }
}
