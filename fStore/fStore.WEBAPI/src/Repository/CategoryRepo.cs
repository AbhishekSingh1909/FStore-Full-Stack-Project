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
        var category = await _data.FindAsync(id);
        if (category is null)
        {
            return null;
        }

        if (updateObject.Name is not null)
        {
            category.Name = updateObject.Name;
        }
        if (updateObject.ImageUrl is not null)
        {
            category.ImageUrl = updateObject.ImageUrl;
        }
        _data.Update(category);
        await _dbContext.SaveChangesAsync();
        return category;
    }
}
