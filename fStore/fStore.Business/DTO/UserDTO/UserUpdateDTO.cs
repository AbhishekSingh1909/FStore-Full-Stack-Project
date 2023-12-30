using System.ComponentModel;
using fStore.Core;

namespace fStore.Business;
public class UserUpdateDTO
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public Role? Role { get; set; }
}

public class UpdateUserPasswordDTO
{
    public string Password { get; set; }
}
