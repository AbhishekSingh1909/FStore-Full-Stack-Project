using fStore.Core;

namespace fStore.Business;

public class UserReadDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }
    public Role Role { get; set; }
    public AddressReadDTO Address { get; set; }
}
