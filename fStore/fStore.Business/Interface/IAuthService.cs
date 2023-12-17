using fStore.Core;

namespace fStore.Business;

public interface IAuthService
{
    Task<string> Login(LoginParams loginParams);
}
