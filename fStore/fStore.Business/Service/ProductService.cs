using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using fStore.Core;

namespace fStore.Business;

public class ProductService : BaseService<Product, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>, IProductService
{
    ICategoryRepo _categoryRepo;
    IProductRepo _productRepo;
    public ProductService(IProductRepo repo, ICategoryRepo categoryRepo, IMapper mapper) : base(repo, mapper)
    {
        _categoryRepo = categoryRepo;
        IProductRepo productRepo = repo;
    }

    public override async Task<ProductReadDTO> CreateOneAsync(Guid id, ProductCreateDTO createObject)
    {
        var category = await _categoryRepo.GetByIdAsync(createObject.CategoryId);
        if (category is null)
        {
            throw CustomException.NotFoundException("Could not create product because category is not found");
        }
        return await base.CreateOneAsync(id, createObject);
    }



    public override async Task<ProductReadDTO> UpdateOneAsync(Guid id, ProductUpdateDTO updateObject)
    {
        if (updateObject?.CategoryId is not null)
        {
            var category = await _categoryRepo.GetByIdAsync(updateObject.CategoryId.Value);
            if (category is null)
            {
                throw CustomException.NotFoundException("Could not update product because category is not found");
            }
        }

        if (updateObject?.Title != null && updateObject.Title == "" || updateObject?.Description != null && updateObject.Description == "")
        {
            throw CustomException.ProductTitleAndDescriptionShouldBeValidString();
        }

        if (updateObject?.Price != null && updateObject.Price <= 0)
        {
            throw CustomException.PriceCanNotBeZeroOrNegative();
        }

        if (updateObject?.Inventory != null && updateObject.Inventory <= 0)
        {
            throw CustomException.InventaryCanNotBeZeroOrNegative();
        }

        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
        {
            throw CustomException.NotFoundException("product not found");
        }
        var record = _mapper.Map<ProductUpdateDTO, Product>(updateObject);
        if (record is null)
        {
            throw CustomException.UnableToMap("Unable to map product entity");
        }
        var updatedUser = await _repo.UpdateOneAsync(id, record);
        return _mapper.Map<Product, ProductReadDTO>(updatedUser);
    }
}
