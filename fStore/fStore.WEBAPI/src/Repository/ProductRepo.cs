using fStore.Business;
using fStore.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace fStore.WEBAPI;

public class ProductRepo : BaseRepo<Product>, IProductRepo
{
    public ProductRepo(DataBaseContext dataBaseContext) : base(dataBaseContext)
    {
    }

    public override async Task<IEnumerable<Product>> GetAllAsync(GetAllParams options)
    {
        var query = _data.AsNoTracking().Include(p => p.Category).Include(p => p.Images).Where(p => p.Title.ToLower().Contains(options.Search.ToLower())).AsQueryable();
        if (options.Limit > 0 && options.Offset >= 0)
        {
            return await query.Skip(options.Offset).Take(options.Limit).ToListAsync();
        }
        return await query.ToListAsync();
    }

    public override async Task<Product?> GetByIdAsync(Guid id)
    {
        var product = await _data.AsNoTracking().Include(p => p.Category).Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
        return product;
    }



    public override async Task<Product> UpdateOneAsync(Guid id, Product updateObject)
    {
        try
        {
            var product = await _data.FindAsync(id);

            if (product != null)
            {
                if (!string.IsNullOrWhiteSpace(updateObject?.Title) && !string.IsNullOrEmpty(updateObject?.Title))
                {
                    product.Title = updateObject.Title;
                }
                if (!string.IsNullOrWhiteSpace(updateObject?.Description) && !string.IsNullOrEmpty(updateObject?.Description))
                {
                    product.Description = updateObject.Description;
                }

                if (updateObject?.Price > 0)
                {
                    product.Price = updateObject.Price;
                }

                if (updateObject?.Inventory > 0)
                {
                    product.Inventory = updateObject.Inventory;
                }
                var newGuid = new Guid();
                if (updateObject?.CategoryId != null && updateObject?.CategoryId != newGuid)
                {
                    product.CategoryId = updateObject.CategoryId;
                }
                _data.Update(product);
                await _dbContext.SaveChangesAsync();
                return product;
            }
            throw CustomException.NotFoundException("Product not found");
        }
        catch (CustomException e)
        {
            Console.WriteLine(e.Message);
            throw CustomException.NotFoundException(e.Message);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
            throw new InvalidOperationException(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(Guid id)
    {
        var query = _data.AsNoTracking().Include(u => u.Category).Include(u => u.Images).Where(u => u.CategoryId == id).AsQueryable();

        return await query.ToListAsync();
    }
}
