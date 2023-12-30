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

    public override async Task<UserReadDTO> CreateOneAsync(Guid id, UserCreateDTO createObject)
    {
        var isEmailAvaialble = await _userRepo.IsEmailAvailableAsync(createObject.Email);
        if (isEmailAvaialble)
        {
            throw CustomException.EmailAvailable($"Email {createObject.Email} is available in system.");
        }
        PasswordService.HashPassword(createObject.Password, out string hashedPassword, out byte[] salt);
        var user = _mapper.Map<UserCreateDTO, User>(createObject);
        user.Password = hashedPassword;
        user.Salt = salt;
        return _mapper.Map<User, UserReadDTO>(await _repo.CreateOneAsync(user));
    }

    public async Task<bool> IsEmailAvailable(string email)
    {
        return await _userRepo.IsEmailAvailableAsync(email);
    }

    public async Task<bool> UpdatePasswordAsync(string newPassword, Guid id)
    {
        var foundUser = await _repo.GetByIdAsync(id);
        if (foundUser is null)
        {
            throw CustomException.NotFoundException(string.Format($"User {id} does not exit"));
        }
        PasswordService.HashPassword(newPassword, out string hashedPassword, out byte[] salt);
        foundUser.Password = hashedPassword;
        foundUser.Salt = salt;
        var updatedUser = await _repo.UpdateOneAsync(id, foundUser);
        if (updatedUser is null)
        {
            throw CustomException.NotFoundException(string.Format($"User {id} does not exit"));
        }
        return true;
    }

    public async override Task<UserReadDTO> UpdateOneAsync(Guid id, UserUpdateDTO updateObject)
    {
        User? entity = await _repo.GetByIdAsync(id);
        if (entity is null)
        {
            throw CustomException.NotFoundException("User is not found");
        }
        var isEmail = await CheckUserMail(updateObject.Email, entity.Email);

        if (!isEmail)
        {
            User record = _mapper.Map<UserUpdateDTO, User>(updateObject, entity);
            var updatedUser = await _repo.UpdateOneAsync(id, record);
            return _mapper.Map<User, UserReadDTO>(updatedUser);
        }
        throw CustomException.EmailAvailable($"Email {updateObject.Email} is available in system.");
    }

    private async Task<bool> CheckUserMail(string newEmail, string oldEmail)
    {
        if (newEmail != null && newEmail != oldEmail)
        {
            var result = await _userRepo.IsEmailAvailableAsync(newEmail);
            return result;
        }
        return false;
    }
}
