using fStore.Core;

namespace fStore.Business;

public interface IImageService : IBaseService<Image, ImageReadDTO, ImageCreateDTO, ImageUpdateDTO>
{
    Task<IEnumerable<ImageReadDTO>> GetImagesByProductId(Guid id);
}
