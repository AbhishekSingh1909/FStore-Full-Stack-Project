using fStore.Core;

namespace fStore.Business;

public interface IOrderService : IBaseService<Order, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>
{
    Task<IEnumerable<OrderReadDTO>> GetUserAllOrdersAsync(Guid id);
}
