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
        return await _data.AsNoTracking().Skip(options.Offset).Take(options.Limit).ToArrayAsync();
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
