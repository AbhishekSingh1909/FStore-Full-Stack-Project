namespace fStore.Core;

public interface IImageRepo : IBaseRepo<Image>
{
    Task<IEnumerable<Image>> GetImagesByProductId(Guid id);
}
