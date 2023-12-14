using System.ComponentModel.DataAnnotations;

namespace fStore.Core;

public class CartItem : BaseEntity
{
    public required int UserId { get; set; }
    public required int ProductId { get; set; }
    public required int Quntity { get; set; }
    public IEnumerable<Product> Products { get; set; }
}
