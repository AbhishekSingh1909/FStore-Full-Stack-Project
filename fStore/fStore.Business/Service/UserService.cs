using AutoMapper;
using fStore.Core;

namespace fStore.Business;

public class UserService : BaseService<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>, IUserService
{
    IUserRepo _userRepo;
    public UserService(IUserRepo repo, IMapper mapper) : base(repo, mapper)
    {
        _userRepo = repo;
    }

    public override async Task<UserReadDTO> CreateOneAsync(UserCreateDTO createObject)
    {
        PasswordService.HashPassword(createObject.Password, out string hashedPassword, out byte[] salt);
        var user = _mapper.Map<UserCreateDTO, User>(createObject);
        user.Password = hashedPassword;
        user.Salt = salt;
        return _mapper.Map<User, UserReadDTO>(await _repo.CreateOneAsync(user));
    }

    public async Task<bool> IsEmailAvailable(string email)
    {
        return await _userRepo.IsEmailAvailable(email);
    }

    public async Task<bool> UpdatePasswordAsync(string newPassword, Guid id)
    {
        var foundUser = await _repo.GetByIdAsync(id);
        if (foundUser is null)
        {
            throw new Exception("User not found");
        }
        PasswordService.HashPassword(newPassword, out string hashedPassword, out byte[] salt);
        foundUser.Password = hashedPassword;
        foundUser.Salt = salt;
        var updatedUser = await _repo.UpdateOneAsync(id, foundUser);
        if (updatedUser is null)
        {
            throw new Exception("user password is not updated");
        }
        return true;
    }
}
