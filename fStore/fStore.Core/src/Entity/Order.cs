namespace fStore.Core;

public class Order : BaseEntity
{
    public User User { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public IEnumerable<OrderProduct> OrderDetails { get; set; }
}
