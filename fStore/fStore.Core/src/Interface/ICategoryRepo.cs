namespace fStore.Core;

public interface ICategoryRepo : IBaseRepo<Category>
{
    Task<IEnumerable<Product>> GetProductsByCategory(Guid id);
}
