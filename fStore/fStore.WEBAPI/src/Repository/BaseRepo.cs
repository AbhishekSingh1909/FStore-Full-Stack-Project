using System.Runtime.CompilerServices;
using fStore.Core;
using Microsoft.EntityFrameworkCore;

namespace fStore.WEBAPI;
public abstract class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity
{
    protected readonly DbSet<T> _data;
    protected readonly DataBaseContext _dbContext;
    public BaseRepo(DataBaseContext dataBaseContext)
    {
        _dbContext = dataBaseContext;
        _data = _dbContext.Set<T>();
    }
    public async Task<T> CreateOneAsync(T createObject)
    {
        await _data.AddAsync(createObject);
        await _dbContext.SaveChangesAsync();
        return createObject;
    }

    public async Task<bool> DeleteByIdAsync(T deleteObject)
    {
        var result = await _data.FindAsync(deleteObject.Id);
        if (result is null)
        {
            return false;
        }
        _data.Remove(deleteObject);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(GetAllParams options)
    {
        var result = await _data.AsNoTracking().Skip(options.Offset).Take(options.Limit).ToListAsync();
        return result;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var result = await _data.FindAsync(id);
        return result;
    }

    public abstract Task<T> UpdateOneAsync(Guid id, T updateObject);
}
