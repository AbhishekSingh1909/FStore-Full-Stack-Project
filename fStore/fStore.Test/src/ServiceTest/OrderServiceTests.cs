using AutoMapper;
using fStore.Business;
using fStore.Core;
using Moq;


namespace fStore.Test;

public class OrderServiceTests
{
    public OrderServiceTests()
    {

    }
    private static IMapper GetMapper()
    {
        MapperConfiguration mappingConfig = new MapperConfiguration(m =>
        {
            m.AddProfile(new MapperProfile());
        });
        IMapper mapper = mappingConfig.CreateMapper();
        return mapper;
    }

    [Theory]
    [ClassData(typeof(GetAllOrdersData))]
    public async void GetAll_ShouldReturn_ValidResponse(IEnumerable<Order> response, IEnumerable<OrderReadDTO> expected)
    {
        Mock<IOrderRepo> repo = new Mock<IOrderRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        GetAllParams options = new GetAllParams();
        repo.Setup(repo => repo.GetAllAsync(options)).Returns(Task.FromResult(response));
        OrderService service = new OrderService(repo.Object, userRepo.Object, productRepo.Object, GetMapper());

        IEnumerable<OrderReadDTO> result = await service.GetAllAsync(options);

        Assert.Equivalent(expected, result);
    }

    [Theory]
    [ClassData(typeof(GetOrderById))]
    public async void GetOneByID_ShouldReturn_ValidResponse(Order response, OrderReadDTO expected, Type? exception)
    {
        Mock<IOrderRepo> repo = new Mock<IOrderRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        OrderService service = new OrderService(repo.Object, userRepo.Object, productRepo.Object, GetMapper());

        if (exception is not null)
        {
            await Assert.ThrowsAsync(exception, () => service.GetByIdAsync(It.IsAny<Guid>()));
        }
        else
        {
            OrderReadDTO result = await service.GetByIdAsync(It.IsAny<Guid>());
            Assert.Equivalent(expected, result);
        }
    }

    [Theory]
    [ClassData(typeof(CreateOneOrderData))]
    public async void CreateOne_ShouldReturn_ValidResponse(User foundUser, Product foundProduct, OrderCreateDTO input, Order response, OrderReadDTO expected)
    {
        Mock<IOrderRepo> repo = new Mock<IOrderRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.CreateOneAsync(It.IsAny<Order>())).Returns(Task.FromResult(response));
        userRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundUser));
        productRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundProduct));
        OrderService service = new OrderService(repo.Object, userRepo.Object, productRepo.Object, GetMapper());
        OrderReadDTO result = await service.CreateOneAsync(It.IsAny<Guid>(), input);

        Assert.Equivalent(expected, result);
    }

    [Theory]
    [ClassData(typeof(DeleteOrderData))]
    public async void DeleteOrderAsync_ShouldReturn_ValidResponse(Order? foundResponse, bool repoResponse, bool? expected, Type? exceptionType)
    {
        var repo = new Mock<IOrderRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(foundResponse);
        repo.Setup(repo => repo.DeleteByIdAsync(It.IsAny<Order>())).ReturnsAsync(repoResponse);
        var productRepo = new Mock<IProductRepo>();
        var userRepo = new Mock<IUserRepo>();
        var orderService = new OrderService(repo.Object, userRepo.Object, productRepo.Object, GetMapper());

        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => orderService.DeleteByIdAsync(It.IsAny<Guid>()));
        }
        else
        {
            var response = await orderService.DeleteByIdAsync(It.IsAny<Guid>());

            Assert.Equivalent(expected, response);
        }
    }

    [Theory]
    [ClassData(typeof(UpdateOneOrderData))]
    public async void UpdateOne_ShouldReturn_ValidResponse(OrderUpdateDTO? input, Order? foundUser, Order? response, OrderReadDTO? expected, Type? exception)
    {
        Mock<IOrderRepo> repo = new Mock<IOrderRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.UpdateOneAsync(It.IsAny<Guid>(), It.IsAny<Order>())).Returns(Task.FromResult(response));
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundUser));
        OrderService service = new OrderService(repo.Object, userRepo.Object, productRepo.Object, GetMapper());
        if (exception != null)
        {
            Assert.ThrowsAsync(exception, async () => await service.UpdateOneAsync(It.IsAny<Guid>(), input));
        }
        else
        {
            OrderReadDTO result = await service.UpdateOneAsync(It.IsAny<Guid>(), input);

            Assert.Equivalent(expected, result);
        }
    }

    public class UpdateOneOrderData : TheoryData<OrderUpdateDTO?, Order?, Order?, OrderReadDTO?, Type?>
    {
        public UpdateOneOrderData()
        {
            OrderUpdateDTO orderInput = new OrderUpdateDTO()
            {
                Status = It.IsAny<OrderStatus>()
            };

            Order order = new Order
            {
                OrderStatus = It.IsAny<OrderStatus>(),
                OrderDetails = new List<OrderProduct>()
            };

            Add(orderInput, order, order, GetMapper().Map<Order, OrderReadDTO>(order), null);
            Add(orderInput, null, null, null, typeof(CustomException));
        }
    }

    public class DeleteOrderData : TheoryData<Order?, bool, bool?, Type?>
    {
        public DeleteOrderData()
        {
            Order order1 = new Order() { OrderStatus = OrderStatus.Pending };
            Add(order1, true, true, null);
            Add(null, false, null, typeof(CustomException));
        }
    }

    public class CreateOneOrderData : TheoryData<User, Product, OrderCreateDTO, Order, OrderReadDTO>
    {
        public CreateOneOrderData()
        {
            OrderCreateDTO orderInput = new OrderCreateDTO()
            {
                OrderProducts = new List<OrderProductCreateDTO>() {
                         new OrderProductCreateDTO { ProductId = It.IsAny<Guid>(), Quntity = 1 } }
            };
            Order order = GetMapper().Map<OrderCreateDTO, Order>(orderInput);
            PasswordService.HashPassword("12345", out string hashedPassword, out byte[] salt);
            User user = new User() { Name = "John Doe", Email = "john@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
            Product product = new Product
            {
                Title = "Product1",
                Description = "Description1",
                Price = 200,
                Inventory = 10,
                CategoryId = It.IsAny<Guid>(),
                Category = It.IsAny<Category>(),
                Images = new List<Image>(),
                Reviews = new List<Review>(),
                OrderProducts = new List<OrderProduct>()
            };
            Add(user, product, orderInput, order, GetMapper().Map<Order, OrderReadDTO>(order));
        }
    }

    public class GetOrderById : TheoryData<Order, OrderReadDTO, Type?>
    {
        public GetOrderById()
        {
            OrderProduct OrderProduct = new OrderProduct
            {
                ProductId = It.IsAny<Guid>(),
                OrderId = It.IsAny<Guid>(),
                Price = 200,
                Quntity = 2,
                Product = It.IsAny<Product>(),
                Order = It.IsAny<Order>()

            };
            Order order = new Order
            {
                OrderStatus = It.IsAny<OrderStatus>(),
                OrderDetails = new List<OrderProduct>() { OrderProduct }
            };
            Add(order, GetMapper().Map<Order, OrderReadDTO>(order), null);
            Add(null, null, typeof(CustomException));
        }
    }

    public class GetAllOrdersData : TheoryData<IEnumerable<Order>, IEnumerable<OrderReadDTO>>
    {
        public GetAllOrdersData()
        {
            IEnumerable<Order> orders = new List<Order>();
            Add(orders, GetMapper().Map<IEnumerable<Order>, IEnumerable<OrderReadDTO>>(orders));
        }
    }
}
