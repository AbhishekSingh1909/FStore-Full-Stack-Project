using fStore.Core;

namespace fStore.Business;

public interface ITokenService
{
    string GenerateToken(User user);
}
