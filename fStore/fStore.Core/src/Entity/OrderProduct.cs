using System.ComponentModel.DataAnnotations;

namespace fStore.Core;

public class OrderProduct
{
    public required int ProductId { get; set; }
    public required int OrderId { get; set; }
    [Range(1, 1000)]
    public required int Quntity { get; set; }
    public decimal TotalPrice { get; set; }
}
