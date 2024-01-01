using AutoMapper;
using fStore.Core;

namespace fStore.Business;

public class OrderService : BaseService<Order, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>, IOrderService
{
    IUserRepo _userRepo;
    IProductRepo _productRepo;
    IOrderRepo _orderRepo;
    public OrderService(IOrderRepo repo, IUserRepo userRepo, IProductRepo productRepo, IMapper mapper) : base(repo, mapper)
    {
        _userRepo = userRepo;
        _productRepo = productRepo;
        _orderRepo = repo;
    }

    public override async Task<OrderReadDTO> CreateOneAsync(Guid id, OrderCreateDTO createObject)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user is null)
        {
            throw CustomException.NotFoundException(string.Format($"User with {id} not found"));
        }

        foreach (OrderProductCreateDTO op in createObject.OrderProducts)
        {
            Product? p = await _productRepo.GetByIdAsync(op.ProductId);
            if (p is null)
            {
                throw CustomException.NotFoundException(string.Format($"Product with {id} not found"));
            }
        }
        var order = _mapper.Map<OrderCreateDTO, Order>(createObject);
        order.UserId = id;
        var createdOrder = await _repo.CreateOneAsync(order);
        return _mapper.Map<Order, OrderReadDTO>(createdOrder);
    }

    public async Task<IEnumerable<OrderReadDTO>> GetUserAllOrdersAsync(Guid id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user is null)
        {
            throw CustomException.NotFoundException(string.Format($"User with {id} not found"));
        }

        var orders = await _orderRepo.GetUserAllOrdersAsync(id);
        return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderReadDTO>>(orders);
    }
}
