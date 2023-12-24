using fStore.Core;
using Microsoft.EntityFrameworkCore;

namespace fStore.WEBAPI;

public class ProductRepo : BaseRepo<Product>, IProductRepo
{
    public ProductRepo(DataBaseContext dataBaseContext) : base(dataBaseContext)
    {
    }

    public override async Task<IEnumerable<Product>> GetAllAsync(GetAllParams options)
    {
        var products = await _data.AsNoTracking().Include(p => p.Category).Include(p => p.Images).Where(p => p.Title.Contains(options.Search) || p.Description.Contains(options.Search)).Skip(options.Offset).Take(options.Limit).ToListAsync();
        return products;
    }

    public override async Task<Product?> GetByIdAsync(Guid id)
    {
        var product = await _data.AsNoTracking().Include(p => p.Category).Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
        return product;
    }

    public override async Task<Product> UpdateOneAsync(Guid id, Product updateObject)
    {
        //var product = await _data.FindAsync(id);
        //if (product is null)
        //{
        //    return null;
        //}

        //if (updateObject.Inventory != product.Inventory)
        //{
        //    product.Inventory = updateObject.Inventory;
        //}
        //if (updateObject.CategoryId != product.CategoryId)
        //{
        //    product.CategoryId = updateObject.CategoryId;
        //}
        //if (updateObject.Title is not null)
        //{
        //    product.Title = updateObject.Title;
        //}
        //if (updateObject.Description is not null)
        //{
        //    product.Description = updateObject.Description;
        //}

        //if (updateObject.Price != product.Price && updateObject.Price > 0)
        //{
        //    product.Price = updateObject.Price;
        //}
        _data.Update(updateObject);
        await _dbContext.SaveChangesAsync();
        return updateObject;
    }
}