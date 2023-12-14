using System.ComponentModel.DataAnnotations;

namespace fStore.Core;

public class Review : BaseEntity
{
    public User User { get; set; }
    public Product Product { get; set; }
    public string? ReviewText { get; set; }
    [Range(1, 5)]
    public required int Rating { get; set; }
}
