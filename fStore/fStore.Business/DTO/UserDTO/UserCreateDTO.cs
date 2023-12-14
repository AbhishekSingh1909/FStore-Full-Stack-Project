using fStore.Core;

namespace fStore.Business;

public class UserCreateDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Avatar { get; set; }
    public Role? Role { get; set; } = Core.Role.Customer;
}
