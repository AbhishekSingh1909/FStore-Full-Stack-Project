namespace fStore.Core;

public class Image : BaseEntity
{
    public required string ImageUrl { get; set; }
    public Product Product { get; set; }
}
