using fStore.Core;

namespace fStore.Business;

public class OrderReadDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserReadDTO User { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<OrderProductReadDTO> OrderProducts { get; set; }

}

public class OrderCreateDTO
{
    public IEnumerable<OrderProductCreateDTO> OrderProducts { get; set; }
}

public class OrderUpdateDTO
{
    public OrderStatus? Status { get; set; }
}
