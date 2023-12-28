using fStore.Core;
using Microsoft.EntityFrameworkCore;

namespace fStore.WEBAPI;
public class ImageRepo : BaseRepo<Image>, IImageRepo
{
    public ImageRepo(DataBaseContext dataBaseContext) : base(dataBaseContext)
    {
    }

    public override async Task<IEnumerable<Image>> GetAllAsync(GetAllParams options)
    {
        var query = _data.AsNoTracking().Where(p => p.ImageUrl.ToLower().Contains(options.Search.ToLower())).AsQueryable();
        if (options.Limit > 0 && options.Offset >= 0)
        { 
            return await query.Skip(options.Offset).Take(options.Limit).ToArrayAsync();
        }
            return await query.ToArrayAsync();
    }

    public override async Task<Image?> GetByIdAsync(Guid id)
    {
        return await _data.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
    }
    public async Task<IEnumerable<Image>> GetImagesByProductId(Guid id)
    {
        var images = await _data.AsNoTracking().Where(m => m.Product.Id == id).ToArrayAsync();
        return images;
    }

    public override async Task<Image> UpdateOneAsync(Guid id, Image updateObject)
    {
        _data.Update(updateObject);
        await _dbContext.SaveChangesAsync();
        return updateObject;
    }
}
