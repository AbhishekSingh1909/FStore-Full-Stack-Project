using AutoMapper;
using fStore.Business;
using fStore.Business.DTO;
using fStore.Core;
using Moq;

namespace fStore.Test;
public class CategoryServiceTests
{
    public CategoryServiceTests()
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
    [Fact]
    public async void GetAllAsync_ShouldInvokeRepoMethod()
    {
        var repo = new Mock<ICategoryRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        var mapper = new Mock<IMapper>();
        var categoryService = new CategoryService(repo.Object, productRepo.Object, GetMapper());
        //GetAllParams options = new GetAllParams() { Limit = 10, Offset = 0 };
        GetAllParams options = new GetAllParams() { };

        await categoryService.GetAllAsync(options);

        repo.Verify(repo => repo.GetAllAsync(options), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetAllCategoriesData))]
    public async void GetAllAsync_ShouldReturn_ValidResponse(IEnumerable<Category> repoResponse, IEnumerable<CategoryReadDTO> expected)
    {
        var repo = new Mock<ICategoryRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        //GetAllParams options = new GetAllParams() { Limit = 10, Offset = 0 };
        GetAllParams options = new GetAllParams() {  };
        repo.Setup(repo => repo.GetAllAsync(options)).ReturnsAsync(repoResponse);
        var categoryService = new CategoryService(repo.Object,productRepo.Object, GetMapper());

        var response = await categoryService.GetAllAsync(options);

        Assert.Equivalent(expected, response);
    }

    [Fact]
    public async void GetCategoryById_ShouldInvoke_RepoMethod()
    {
        var repo = new Mock<ICategoryRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        Category category1 = new Category() { Name = "Air plants", ImageUrl = "https://picsum.photos/200" };
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category1);
        var mapper = new Mock<IMapper>();
        var categoryService = new CategoryService(repo.Object,productRepo.Object, GetMapper());

        await categoryService.GetByIdAsync(It.IsAny<Guid>());


        repo.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetOneCategoryData))]
    public async void GetCategoryById_ShouldReturn_ValidResponse(Category? repoResponse, CategoryReadDTO? expected, Type? exceptionType)
    {
        var repo = new Mock<ICategoryRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(repoResponse));
        var categoryService = new CategoryService(repo.Object, productRepo.Object, GetMapper());

        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => categoryService.GetByIdAsync(It.IsAny<Guid>()));
        }
        else
        {
            var response = await categoryService.GetByIdAsync(It.IsAny<Guid>());

            Assert.Equivalent(expected, response);
        }
    }

    [Fact]
    public async void CreateOneAsync_ShouldInvoke_RepoMethod()
    {
        var repo = new Mock<ICategoryRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        var mapper = new Mock<IMapper>();
        var categoryService = new CategoryService(repo.Object,productRepo.Object, GetMapper());
        CategoryCreateDTO dto = new CategoryCreateDTO() { Name = "Air plants", ImageUrl = "https://picsum.photos/200" };

        await categoryService.CreateOneAsync(new Guid(), dto);

        repo.Verify(repo => repo.CreateOneAsync(It.IsAny<Category>()), Times.Once);
    }

    [Theory]
    [ClassData(typeof(CreateCategoryData))]
    public async void CreateOneAsync_ShouldReturn_ValidResponse(Category repoResponse, CategoryReadDTO expected, Type? exceptionType)
    {
        var repo = new Mock<ICategoryRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.CreateOneAsync(It.IsAny<Category>())).ReturnsAsync(repoResponse);
        var categoryService = new CategoryService(repo.Object,productRepo.Object, GetMapper());
        CategoryCreateDTO dto = new CategoryCreateDTO() { Name = "Air plants", ImageUrl = "https://picsum.photos/200" };

        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => categoryService.CreateOneAsync(new Guid(), dto));
        }
        else
        {
            var response = await categoryService.CreateOneAsync(new Guid(), dto);

            Assert.Equivalent(expected, response);
        }
    }

    [Fact]
    public async void DeleteCategoryAsync_ShouldInvoke_RepoMethod()
    {
        var repo = new Mock<ICategoryRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        Category category1 = new Category() { Name = "Air plants", ImageUrl = "https://picsum.photos/200" };
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category1);
        var mapper = new Mock<IMapper>();
        var categoryService = new CategoryService(repo.Object,productRepo.Object, GetMapper());

        await categoryService.DeleteByIdAsync(It.IsAny<Guid>());

        repo.Verify(repo => repo.DeleteByIdAsync(It.IsAny<Category>()), Times.Once);
    }

    [Theory]
    [ClassData(typeof(DeleteCategoryData))]
    public async void DeleteCategoryAsync_ShouldReturn_ValidResponse(Category? foundResponse, bool repoResponse, bool? expected, Type? exceptionType)
    {
        var repo = new Mock<ICategoryRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(foundResponse);
        repo.Setup(repo => repo.DeleteByIdAsync(It.IsAny<Category>())).ReturnsAsync(repoResponse);
        var categoryService = new CategoryService(repo.Object,productRepo.Object, GetMapper());

        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => categoryService.DeleteByIdAsync(It.IsAny<Guid>()));
        }
        else
        {
            var response = await categoryService.DeleteByIdAsync(It.IsAny<Guid>());

            Assert.Equivalent(expected, response);
        }
    }

    [Fact]
    public async void UpdateCategoryAsync_ShouldInvoke_RepoMethod()
    {
        var repo = new Mock<ICategoryRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        Category category1 = new Category() { Name = "Air plants", ImageUrl = "https://picsum.photos/200" };
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category1);
        var mapper = new Mock<IMapper>();
        var categoryService = new CategoryService(repo.Object, productRepo.Object, GetMapper());
        CategoryUpdateDTO updates = new CategoryUpdateDTO() { Name = "Tillandsia", ImageUrl = "https://picsum.photos/300" };

        await categoryService.UpdateOneAsync(It.IsAny<Guid>(), updates);

        repo.Verify(repo => repo.UpdateOneAsync(It.IsAny<Guid>(), It.IsAny<Category>()), Times.Once);
    }

    [Theory]
    [ClassData(typeof(UpdateCategoryData))]
    public async void UpdateCategoryAsync_ShouldReturn_ValidResponse(Category? foundResponse, Category repoResponse, CategoryReadDTO? expected, Type? exceptionType)
    {
        var repo = new Mock<ICategoryRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(foundResponse);
        repo.Setup(repo => repo.UpdateOneAsync(It.IsAny<Guid>(), It.IsAny<Category>())).ReturnsAsync(repoResponse);
        var categoryService = new CategoryService(repo.Object,productRepo.Object, GetMapper());
        CategoryUpdateDTO updates = new CategoryUpdateDTO() { Name = "Tillandsia", ImageUrl = "https://i.imgur.com/LDOO4Qs.jpg" };

        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => categoryService.UpdateOneAsync(It.IsAny<Guid>(), updates));
        }
        else
        {
            var response = await categoryService.UpdateOneAsync(It.IsAny<Guid>(), updates);

            Assert.Equivalent(expected, response);
        }
    }

    public class UpdateCategoryData : TheoryData<Category, Category, CategoryReadDTO, Type?>
    {
        public UpdateCategoryData()
        {
            Category category = new Category { Name = "Tillandsia", ImageUrl = "https://picsum.photos/300" };
            Category updatesCategory = new Category { Name = "Tillandsia", ImageUrl = "https://i.imgur.com/LDOO4Qs.jpg" };
            CategoryReadDTO customerReadDto = GetMapper().Map<Category, CategoryReadDTO>(updatesCategory);
            Add(category, updatesCategory, customerReadDto, null);
            Add(null, null, null, typeof(CustomException));
        }
    }

    public class DeleteCategoryData : TheoryData<Category?, bool, bool?, Type?>
    {
        public DeleteCategoryData()
        {
            Category category1 = new Category() { Name = "Air plants", ImageUrl = "https://picsum.photos/200" };
            Add(category1, true, true, null);
            Add(null, false, null, typeof(CustomException));
        }
    }

    public class CreateCategoryData : TheoryData<Category?, CategoryReadDTO?, Type?>
    {
        public CreateCategoryData()
        {
            Category category1 = new Category() { Name = "Air plants", ImageUrl = "https://picsum.photos/200" };
            CategoryReadDTO category1Read = GetMapper().Map<Category, CategoryReadDTO>(category1);
            Add(category1, category1Read, null);
        }
    }

    public class GetOneCategoryData : TheoryData<Category?, CategoryReadDTO?, Type?>
    {
        public GetOneCategoryData()
        {
            Category category = new Category() { Name = "Air plants", ImageUrl = "https://picsum.photos/200" };
            CategoryReadDTO category1Read = GetMapper().Map<Category, CategoryReadDTO>(category);
            Add(category, category1Read, null);
            Add(null, null, typeof(CustomException));
        }
    }

    public class GetAllCategoriesData : TheoryData<IEnumerable<Category>, IEnumerable<CategoryReadDTO>>
    {
        public GetAllCategoriesData()
        {
            Category category1 = new Category() { Name = "Air plants", ImageUrl = "https://picsum.photos/200" };
            Category category2 = new Category() { Name = "Succulents", ImageUrl = "https://picsum.photos/200" };
            Category category3 = new Category() { Name = "Cacti", ImageUrl = "https://picsum.photos/200" };
            IEnumerable<Category> categories = new List<Category>() { category1, category2, category3 };
            Add(categories, GetMapper().Map<IEnumerable<Category>, IEnumerable<CategoryReadDTO>>(categories));
        }
    }
}
