using fStore.Core;

namespace fStore.Business;

public interface IAuthService
{
    Task<string> LoginAsync(LoginParams loginParams);
}
