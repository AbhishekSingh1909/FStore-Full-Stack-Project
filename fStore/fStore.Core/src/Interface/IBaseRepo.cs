namespace fStore.Core;

public interface IBaseRepo<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(GetAllParams options);
    Task<T?> GetByIdAsync(Guid id);
    Task<T> CreateOneAsync(T createObject);
    Task<T> UpdateOneAsync(Guid id,T updateObject);
    Task<bool> DeleteByIdAsync(T deleteObject);
}
