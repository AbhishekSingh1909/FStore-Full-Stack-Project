using AutoMapper;
using fStore.Business;
using fStore.Core;
using Moq;

namespace fStore.Test;

public class ProductServiceTest
{
    public ProductServiceTest()
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
    [ClassData(typeof(GetAllProductsData))]
    public async void GetAllAsync_ShouldReturn_ValidResponse(IEnumerable<Product> response, IEnumerable<ProductReadDTO> expected)
    {
        Mock<IProductRepo> repo = new Mock<IProductRepo>();
        Mock<ICategoryRepo> categoryRepo = new Mock<ICategoryRepo>();
        GetAllParams options = new GetAllParams();
        repo.Setup(repo => repo.GetAllAsync(options)).Returns(Task.FromResult(response));
        ProductService service = new ProductService(repo.Object, categoryRepo.Object, GetMapper());

        IEnumerable<ProductReadDTO> result = await service.GetAllAsync(options);

        Assert.Equivalent(expected, result);
    }

    [Theory]
    [ClassData(typeof(GetOneByIDData))]
    public async void GetOneByID_ShouldReturn_ValidResponse(Product? repoResponse, ProductReadDTO? expected, Type? exceptionType)
    {
        Mock<IProductRepo> repo = new Mock<IProductRepo>();
        Mock<ICategoryRepo> categoryRepo = new Mock<ICategoryRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(repoResponse));
        ProductService service = new ProductService(repo.Object, categoryRepo.Object, GetMapper());

        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => service.GetByIdAsync(It.IsAny<Guid>()));
        }
        else
        {
            var response = await service.GetByIdAsync(It.IsAny<Guid>());

            Assert.Equivalent(expected, response);
        }
    }

    [Theory]
    [ClassData(typeof(CreateOneProductData))]
    public async void CreateOne_ShouldReturn_ValidResponse(Category foundCategory, ProductCreateDTO input, Product response, ProductReadDTO expected)
    {
        Mock<IProductRepo> repo = new Mock<IProductRepo>();
        Mock<ICategoryRepo> categoryRepo = new Mock<ICategoryRepo>();
        repo.Setup(repo => repo.CreateOneAsync(It.IsAny<Product>())).Returns(Task.FromResult(response));
        categoryRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundCategory));
        ProductService service = new ProductService(repo.Object, categoryRepo.Object, GetMapper());

        ProductReadDTO result = await service.CreateOneAsync(It.IsAny<Guid>(), input);

        Assert.Equivalent(expected, result);
    }

    [Theory]
    [ClassData(typeof(DeleteProductData))]
    public async void DeleteProductAsync_ShouldReturnValidResponse(Product? foundResponse, bool repoResponse, bool? expected, Type? exceptionType)
    {
        var repo = new Mock<IProductRepo>();
        var categoryRepo = new Mock<ICategoryRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(foundResponse);
        repo.Setup(repo => repo.DeleteByIdAsync(It.IsAny<Product>())).ReturnsAsync(repoResponse);
        var productService = new ProductService(repo.Object, categoryRepo.Object, GetMapper());

        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => productService.DeleteByIdAsync(It.IsAny<Guid>()));
        }
        else
        {
            var response = await productService.DeleteByIdAsync(It.IsAny<Guid>());

            Assert.Equivalent(expected, response);
        }

    }

    [Theory]
    [ClassData(typeof(UpdateOneProductData))]
    public async void UpdateOne_ShouldReturn_ValidResponse(ProductUpdateDTO? input, Product? foundCategory, Product? response, ProductReadDTO? expected, Type? exception)
    {
        Mock<IProductRepo> repo = new Mock<IProductRepo>();
        Mock<ICategoryRepo> categoryRepo = new Mock<ICategoryRepo>();
        repo.Setup(repo => repo.UpdateOneAsync(It.IsAny<Guid>(), It.IsAny<Product>())).Returns(Task.FromResult(response));
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundCategory));
        ProductService service = new ProductService(repo.Object, categoryRepo.Object, GetMapper());

        if (exception is not null)
        {
            await Assert.ThrowsAsync(exception, () => service.UpdateOneAsync(It.IsAny<Guid>(), input));
        }
        else
        {
            ProductReadDTO result = await service.UpdateOneAsync(It.IsAny<Guid>(), input);

            Assert.Equivalent(expected, result);
        }
    }

    public class UpdateOneProductData : TheoryData<ProductUpdateDTO?, Product?, Product?, ProductReadDTO?, Type?>
    {
        public UpdateOneProductData()
        {
            ProductUpdateDTO productInput = new ProductUpdateDTO()
            {
                Price = 20,
            };

            Product product = new Product()
            {
                Title = "Product1",
                Description = "Description1",
                Price = 10,
                Inventory = 1,
                CategoryId = It.IsAny<Guid>(),
                Category = It.IsAny<Category>(),
                Images = new List<Image>(),
                Reviews = new List<Review>(),
                OrderProducts = new List<OrderProduct>()
            };
            Product updatedproduct = new Product()
            {
                Title = "Product1",
                Description = "Description1",
                Price = 20,
                Inventory = 1,
                CategoryId = It.IsAny<Guid>(),
                Category = It.IsAny<Category>(),
                Images = new List<Image>(),
                Reviews = new List<Review>(),
                OrderProducts = new List<OrderProduct>()
            };
            Add(productInput, product, updatedproduct, GetMapper().Map<Product, ProductReadDTO>(updatedproduct), null);
            Add(productInput, null, null, null, typeof(CustomException));
        }
    }


    public class DeleteProductData : TheoryData<Product?, bool, bool?, Type?>
    {
        public DeleteProductData()
        {
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
            Add(product, true, true, null);
            Add(null, false, null, typeof(CustomException));
        }
    }

    public class CreateOneProductData : TheoryData<Category, ProductCreateDTO, Product, ProductReadDTO>
    {
        public CreateOneProductData()
        {
            ProductCreateDTO productInput = new ProductCreateDTO()
            {
                Title = "Product1",
                Description = "Description1",
                Price = 0,
                Inventory = 1,
                CategoryId = It.IsAny<Guid>(),
                Images = new List<ImageCreateDTO>(),
            };
            Product product = GetMapper().Map<ProductCreateDTO, Product>(productInput);
            Category category = new Category() { Name = "Air plants", ImageUrl = "https://picsum.photos/200" };
            Add(category, productInput, product, GetMapper().Map<Product, ProductReadDTO>(product));
        }
    }

    public class GetOneByIDData : TheoryData<Product?, ProductReadDTO?, Type?>
    {
        public GetOneByIDData()
        {
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
            Add(product, GetMapper().Map<Product, ProductReadDTO>(product), null);
            Add(null, null, typeof(CustomException));
        }
    }

    public class GetAllProductsData : TheoryData<IEnumerable<Product>, IEnumerable<ProductReadDTO>>
    {
        public GetAllProductsData()
        {

            Product product = new Product()
            {
                Title = "Product1",
                Description = "Description1",
                Price = 100,
                Inventory = 1,
                CategoryId = It.IsAny<Guid>(),
                Category = It.IsAny<Category>(),
                Images = new List<Image>(),
                Reviews = new List<Review>(),
                OrderProducts = new List<OrderProduct>()
            };
            IEnumerable<Product> products = new List<Product>() { product };
            Add(products, GetMapper().Map<IEnumerable<Product>, IEnumerable<ProductReadDTO>>(products));
        }
    }
}
