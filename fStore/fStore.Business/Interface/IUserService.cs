using fStore.Core;

namespace fStore.Business;
    public interface IUserService
    {
         IEnumerable<UserReadDTO> GetAllUsers(GetAllParams options);
         UserReadDTO GetUserById (Guid id);
         public UserReadDTO CreateUser(UserCreateDTO user);
    }
