namespace fStore.Core;
    public interface IUserRepo
    {
        IEnumerable<User> GetAllUsers(GetAllParams options);
        User GetUserById (Guid id);
        User CreateUser (User user);
    }
