namespace fStore.Core;

public interface IProductRepo : IBaseRepo<Product>
{
    Task<IEnumerable<Product>> GetProductsByCategory(Guid id);
}
