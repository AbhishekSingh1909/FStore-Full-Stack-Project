using AutoMapper;
using fStore.Business.DTO;
using fStore.Core;

namespace fStore.Business;

public class CategoryService : BaseService<Category, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>, ICategoryService
{
    IProductRepo _productRepo;
    public CategoryService(ICategoryRepo repo, IProductRepo productRepo, IMapper mapper) : base(repo, mapper)
    {
        _productRepo = productRepo;
    }
    public async Task<IEnumerable<ProductReadDTO>> GetProductsByCategory(Guid id)
    {
        var products = await _productRepo.GetProductsByCategory(id);
        if (products is null)
        {
            throw CustomException.NotFoundException($"No product found by Category {id}");
        }
        var results = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductReadDTO>>(products);
        return results;
    }
}
