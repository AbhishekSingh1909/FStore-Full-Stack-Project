using fStore.Core;

namespace fStore.Business;
public interface IUserService : IBaseService<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>
{
    Task<bool> UpdatePasswordAsync(string newPassword, Guid id);
    Task<bool> IsEmailAvailable(string email);
}
