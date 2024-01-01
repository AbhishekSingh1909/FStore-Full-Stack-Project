using fStore.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace fStore.WEBAPI;

public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
{
    private readonly DbSet<Product> _product;
    public CategoryRepo(DataBaseContext dataBaseContext) : base(dataBaseContext)
    {
        _product = dataBaseContext.Products;
    }

    public override async Task<IEnumerable<Category>> GetAllAsync(GetAllParams options)
    {
        var query = _data.AsNoTracking().Where(c => c.Name.ToLower().Contains(options.Search.ToLower()));
        if (options.Limit > 0 && options.Offset >= 0)
        {
          return await query.Skip(options.Offset).Take(options.Limit).ToListAsync();
        }
          return await query.ToListAsync();
       
    }

    public override async Task<Category?> GetByIdAsync(Guid id)
    {
        var category = await _data.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return category;
    }

    public override async Task<Category> UpdateOneAsync(Guid id, Category updateObject)
    {

        _data.Update(updateObject);
        await _dbContext.SaveChangesAsync();
        return updateObject;
    }
    public async Task<IEnumerable<Product>> GetProductsByCategory(Guid id)
    {
        var product =  _product.Include(p => p.Category).Include(p => p.Images).Where(p=> p.CategoryId == id).AsQueryable();
        var  products = await product.ToListAsync();
        return products;
    }
}
