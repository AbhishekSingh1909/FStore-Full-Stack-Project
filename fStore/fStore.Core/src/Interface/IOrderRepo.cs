namespace fStore.Core;

public interface IOrderRepo : IBaseRepo<Order>
{
    Task<IEnumerable<Order>> GetUserAllOrdersAsync(Guid id);
}
