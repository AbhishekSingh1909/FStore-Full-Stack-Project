using System.ComponentModel.DataAnnotations;

namespace fStore.Core;
public class User : BaseEntity
{
    [StringLength(100, MinimumLength = 1)]
    public required string Name { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required byte[] Salt { get; set; }
    public string? Avatar { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    public required Role Role { get; set; }
    public Address Address { get; set; }
    public IEnumerable<Order> Orders { get; set; }
    public IEnumerable<Review> Reviews { get; set; }
}
