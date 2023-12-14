namespace fStore.Core;

public class Category : BaseEntity
{
    public required string Name { get; set; }
    public string? ImageUrl { get; set; }
    //Navigation property
    public IEnumerable<Product> Products { get; set; }
}
