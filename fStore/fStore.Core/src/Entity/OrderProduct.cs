using System.ComponentModel.DataAnnotations;

namespace fStore.Core;

public class OrderProduct
{
    public required Guid ProductId { get; set; }
    public required Guid OrderId { get; set; }
    [Range(1, 1000)]
    public required int Quntity { get; set; }
    public decimal TotalPrice { get; set; }
}
