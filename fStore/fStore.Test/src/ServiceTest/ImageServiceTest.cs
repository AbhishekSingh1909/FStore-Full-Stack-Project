using AutoMapper;
using fStore.Business;
using fStore.Core;
using Moq;

namespace fStore.Test;

public class ImageServiceTest
{
    public ImageServiceTest()
    {

    }

    private static IMapper GetMapper()
    {
        MapperConfiguration mappingConfig = new MapperConfiguration(m =>
        {
            m.AddProfile(new MapperProfile());
        });
        IMapper mapper = mappingConfig.CreateMapper();
        return mapper;
    }

    [Theory]
    [ClassData(typeof(GetAllImagesData))]
    public async void GetAll_ShouldReturn_ValidResponse(IEnumerable<Image> response, IEnumerable<ImageReadDTO> expected)
    {
        Mock<IImageRepo> repo = new Mock<IImageRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        GetAllParams options = new GetAllParams();
        repo.Setup(repo => repo.GetAllAsync(options)).Returns(Task.FromResult(response));
        ImageService service = new ImageService(repo.Object, productRepo.Object, GetMapper());

        IEnumerable<ImageReadDTO> result = await service.GetAllAsync(options);

        Assert.Equivalent(expected, result);
    }

    [Theory]
    [ClassData(typeof(GetIamgeByIdData))]
    public async void GetOneByID_ShouldReturn_ValidResponse(Image response, ImageReadDTO expected, Type? exception)
    {
        Mock<IImageRepo> repo = new Mock<IImageRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        ImageService service = new ImageService(repo.Object, productRepo.Object, GetMapper());

        if (exception != null)
        {
            Assert.ThrowsAsync(exception, async () => await service.GetByIdAsync(It.IsAny<Guid>()));
        }
        else
        {
            ImageReadDTO result = await service.GetByIdAsync(It.IsAny<Guid>());

            Assert.Equivalent(expected, response);
        }
    }

    [Theory]
    [ClassData(typeof(CreateOneImageData))]
    public async void CreateOne_ShouldReturn_ValidResponse(ImageCreateDTO input, Image response, ImageReadDTO expected)
    {
        Mock<IImageRepo> repo = new Mock<IImageRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.CreateOneAsync(It.IsAny<Image>())).Returns(Task.FromResult(response));
        ImageService service = new ImageService(repo.Object, productRepo.Object, GetMapper());

        ImageReadDTO result = await service.CreateOneAsync(It.IsAny<Guid>(), input);

        Assert.Equivalent(expected, result);
    }

    [Theory]
    [ClassData(typeof(UpdateOneImageData))]
    public async void UpdateOne_ShouldReturn_ValidResponse(ImageUpdateDTO? input, Image? foundImage, Image? response, ImageReadDTO? expected, Type? exception)
    {
        Mock<IImageRepo> repo = new Mock<IImageRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.UpdateOneAsync(It.IsAny<Guid>(), It.IsAny<Image>())).Returns(Task.FromResult(response));
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundImage));
        ImageService service = new ImageService(repo.Object, productRepo.Object, GetMapper());

        if (exception is not null)
        {
            Assert.ThrowsAsync(exception, async () => await service.UpdateOneAsync(It.IsAny<Guid>(), input));
        }
        else
        {
            ImageReadDTO result = await service.UpdateOneAsync(It.IsAny<Guid>(), input);

            Assert.Equivalent(expected, result);
        }
    }

    [Theory]
    [ClassData(typeof(DeleteImageData))]
    public async void DeleteOne_ShouldReturn_ValidResponse(Image? foundResponse, bool repoResponse, bool? expected, Type? exceptionType)
    {
        Mock<IImageRepo> repo = new Mock<IImageRepo>();
        Mock<IProductRepo> productRepo = new Mock<IProductRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(foundResponse);
        repo.Setup(repo => repo.DeleteByIdAsync(It.IsAny<Image>())).Returns(Task.FromResult(repoResponse));
        ImageService service = new ImageService(repo.Object, productRepo.Object, GetMapper());
        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => service.DeleteByIdAsync(It.IsAny<Guid>()));
        }
        else
        {
            bool result = await service.DeleteByIdAsync(It.IsAny<Guid>());
            Assert.Equal(expected, result);
        }
    }

    public class DeleteImageData : TheoryData<Image?, bool, bool?, Type?>
    {
        public DeleteImageData()
        {
            Image image = new Image() { ImageUrl = "https://picsum.photos/800" };
            Add(image, true, true, null);
            Add(null, false, null, typeof(CustomException));
        }
    }

    public class UpdateOneImageData : TheoryData<ImageUpdateDTO?, Image?, Image?, ImageReadDTO?, Type?>
    {
        public UpdateOneImageData()
        {
            ImageUpdateDTO imageInput = new ImageUpdateDTO()
            {
                ImageUrl = "https://picsum.photos/200"
            };
            Image image = new Image()
            {
                ImageUrl = "https://picsum.photos/200",
                ProductId = It.IsAny<Guid>()
            };
            Add(imageInput, image, image, GetMapper().Map<Image, ImageReadDTO>(image), null);
            Add(imageInput, null, null, null, typeof(CustomException));
        }
    }


    public class CreateOneImageData : TheoryData<ImageCreateDTO, Image, ImageReadDTO>
    {
        public CreateOneImageData()
        {
            ImageCreateDTO imageInput = new ImageCreateDTO()
            {
                ImageUrl = "https://picsum.photos/200"
            };
            Image image = GetMapper().Map<ImageCreateDTO, Image>(imageInput);
            Add(imageInput, image, GetMapper().Map<Image, ImageReadDTO>(image));
        }
    }
    public class GetIamgeByIdData : TheoryData<Image, ImageReadDTO, Type?>
    {
        public GetIamgeByIdData()
        {
            Image image = new Image()
            {
                ImageUrl = "https://picsum.photos/200",
                ProductId = It.IsAny<Guid>()
            };
            Add(image, GetMapper().Map<Image, ImageReadDTO>(image), null);
            Add(null, null, typeof(CustomException));
        }
    }

    public class GetAllImagesData : TheoryData<IEnumerable<Image>, IEnumerable<ImageReadDTO>>
    {
        public GetAllImagesData()
        {
            IEnumerable<Image> images = new List<Image>();
            Add(images, GetMapper().Map<IEnumerable<Image>, IEnumerable<ImageReadDTO>>(images));
        }
    }
}
