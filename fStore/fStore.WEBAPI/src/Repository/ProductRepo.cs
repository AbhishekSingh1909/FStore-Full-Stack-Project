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

        _data.Update(updateObject);
        await _dbContext.SaveChangesAsync();
        return updateObject;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(Guid id)
    {
        var query = _data.AsNoTracking().Include(u => u.Category).Include(u => u.Images).Where(u => u.CategoryId == id).AsQueryable();

        return await query.ToListAsync();
    }
}
