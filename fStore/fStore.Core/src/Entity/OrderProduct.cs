using System.ComponentModel.DataAnnotations;

namespace fStore.Core;

public class OrderProduct : TimeStamp
{
    public required Guid ProductId { get; set; }
    public required Guid OrderId { get; set; }
    public Product Product { get; set; }
    public Order Order { get; set; }
    [Range(1, 1000)]
    public int Quntity { get; set; }
    public decimal Price { get; set; }
}
