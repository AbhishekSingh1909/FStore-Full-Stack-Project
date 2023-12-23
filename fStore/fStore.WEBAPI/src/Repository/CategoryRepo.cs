using fStore.Core;
using Microsoft.EntityFrameworkCore;

namespace fStore.WEBAPI;

public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
{
    public CategoryRepo(DataBaseContext dataBaseContext) : base(dataBaseContext)
    {
    }

    public override async Task<IEnumerable<Category>> GetAllAsync(GetAllParams options)
    {
        var categories = await _data.AsNoTracking()
        .Where(c => c.Name.Contains(options.Search)).Skip(options.Offset)
        .Take(options.Limit).ToListAsync();
        return categories;
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
}
