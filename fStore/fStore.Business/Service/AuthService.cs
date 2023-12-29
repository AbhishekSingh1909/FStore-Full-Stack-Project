using fStore.Core;

namespace fStore.Business;

public class AuthService : IAuthService
{
    IUserRepo _userRepo;
    ITokenService _tokenService;

    public AuthService(IUserRepo userRepo, ITokenService tokenService)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
    }
    public async Task<string> LoginAsync(LoginParams loginParams)
    {
        var foundUser = await _userRepo.FindByEmailAsync(loginParams.Email);

        if (foundUser is null)
        {
            throw CustomException.NotFoundException("User not found");
        }

        var isPasswordMatch = PasswordService.VerifyPassword(loginParams.Password, foundUser.Password, foundUser.Salt);

        if (isPasswordMatch)
        {
            return _tokenService.GenerateToken(foundUser);
        }

        throw CustomException.WrongCredentialsException("User credential is not valid");
    }
}
