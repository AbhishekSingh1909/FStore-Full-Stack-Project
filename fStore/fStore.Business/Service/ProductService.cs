using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using fStore.Core;

namespace fStore.Business;

public class ProductService : BaseService<Product, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>, IProductService
{
    ICategoryRepo _categoryRepo;
    public ProductService(IProductRepo repo, ICategoryRepo categoryRepo, IMapper mapper) : base(repo, mapper)
    {
        _categoryRepo = categoryRepo;
    }

    public override async Task<ProductReadDTO> CreateOneAsync(ProductCreateDTO createObject)
    {
        var category = await _categoryRepo.GetByIdAsync(createObject.CategoryId);
        if (category is null)
        {
            throw CustomException.NotFoundException("Could not create product because category is not found");
        }
        return await base.CreateOneAsync(createObject);
    }
}
