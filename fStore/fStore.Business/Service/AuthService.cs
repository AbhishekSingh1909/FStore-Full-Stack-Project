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
    public async Task<string> Login(LoginParams loginParams)
    {
        var foundUser = await _userRepo.FindByEmailAsync(loginParams.Email);

        if (foundUser is null)
        {
            throw new Exception("User not found");
        }

        var isPasswordMatch = PasswordService.VerifyPassword(loginParams.Password, foundUser.Password, foundUser.Salt);

        if (isPasswordMatch)
        {
            return _tokenService.GenerateToken(foundUser);
        }

        throw new Exception("User credential is not valid");
    }
}
