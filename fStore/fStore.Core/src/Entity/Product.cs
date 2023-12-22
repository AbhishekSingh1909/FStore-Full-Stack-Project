using System.ComponentModel.DataAnnotations;

namespace fStore.Core;

public class Product : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    [Range(0, 100000)]
    public required decimal Price { get; set; }
    public int Inventory { get; set; }
    public IEnumerable<Image>? Images { get; set; }
    // Navigation Property
    public Category Category { get; set; }
    //Foreign Key
    public required Guid CategoryId { get; set; }
    public IEnumerable<OrderProduct> OrderProducts { get; set; }
    public IEnumerable<Review> Reviews { get; set; }
}
