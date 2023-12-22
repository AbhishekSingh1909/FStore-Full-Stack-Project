using AutoMapper;
using fStore.Core;

namespace fStore.Business;

public class ImageService : BaseService<Image, ImageReadDTO, ImageCreateDTO, ImageUpdateDTO>, IImageService
{
    IProductRepo _productRepo;
    IImageRepo _imageRepo;
    public ImageService(IImageRepo repo, IProductRepo productRepo, IMapper mapper) : base(repo, mapper)
    {
        _productRepo = productRepo;
        _imageRepo = repo;
    }

    public async Task<IEnumerable<ImageReadDTO>> GetImagesByProductId(Guid id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product is null)
        {
            throw CustomException.NotFoundException("product is not found");
        }
        var images = await _imageRepo.GetImagesByProductId(id);
        return _mapper.Map<IEnumerable<Image>, IEnumerable<ImageReadDTO>>(images);
    }
}
