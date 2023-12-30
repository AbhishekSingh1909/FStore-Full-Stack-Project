using AutoMapper;
using fStore.Business;
using fStore.Core;
using Moq;

namespace fStore.Test;

public class AddressServiceTest
{
    public AddressServiceTest()
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
    [ClassData(typeof(GetAllAddressesData))]
    public async void GetAll_ShouldReturn_ValidResponse(IEnumerable<Address> response, IEnumerable<AddressReadDTO> expected)
    {
        Mock<IAddressRepo> repo = new Mock<IAddressRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        GetAllParams options = new GetAllParams();
        repo.Setup(repo => repo.GetAllAsync(options)).Returns(Task.FromResult(response));
        AddressService service = new AddressService(repo.Object, userRepo.Object, GetMapper());

        IEnumerable<AddressReadDTO> result = await service.GetAllAsync(options);

        Assert.Equivalent(expected, result);
    }

    [Theory]
    [ClassData(typeof(GetOneByIdData))]
    public async void GetOneByID_ShouldReturn_ValidResponse(Address response, AddressReadDTO expected, Type? exception)
    {
        Mock<IAddressRepo> repo = new Mock<IAddressRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        AddressService service = new AddressService(repo.Object, userRepo.Object, GetMapper());
        if (exception != null)
        {
            Assert.ThrowsAsync(exception, async () => service.GetByIdAsync(It.IsAny<Guid>()));
        }
        else
        {
            AddressReadDTO result = await service.GetByIdAsync(It.IsAny<Guid>());
            Assert.Equivalent(expected, result);
        }
    }

    [Theory]
    [ClassData(typeof(CreateOneAddressData))]
    public async void CreateOne_ShouldReturn_ValidResponse(AddressCreateDTO input, Address response, AddressReadDTO expected)
    {
        Mock<IAddressRepo> repo = new Mock<IAddressRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.CreateOneAsync(It.IsAny<Address>())).Returns(Task.FromResult(response));
        AddressService service = new AddressService(repo.Object, userRepo.Object, GetMapper());

        AddressReadDTO result = await service.CreateOneAsync(It.IsAny<Guid>(), input);

        Assert.Equivalent(expected, result);
    }

    [Theory]
    [ClassData(typeof(UpdateOneAddressData))]
    public async void UpdateOne_ShouldReturn_ValidResponse(User? user, AddressUpdateDTO? input, Address? foundAddress, Address? response, AddressReadDTO? expected, Type? exception)
    {
        Mock<IAddressRepo> repo = new Mock<IAddressRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        userRepo.Setup(userRepo => userRepo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
        repo.Setup(repo => repo.UpdateOneAsync(It.IsAny<Guid>(), It.IsAny<Address>())).Returns(Task.FromResult(response));
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundAddress));
        AddressService service = new AddressService(repo.Object, userRepo.Object, GetMapper());

        if (exception != null)
        {
            Assert.ThrowsAsync(exception, async () => await service.UpdateOneAsync(It.IsAny<Guid>(), input));
        }
        else
        {
            AddressReadDTO result = await service.UpdateOneAsync(It.IsAny<Guid>(), input);
            Assert.Equivalent(expected, result);
        }
    }

    [Theory]
    [ClassData(typeof(DeleteAddressData))]
    public async void DeleteOne_ShouldReturn_ValidResponse(User? user, Address? foundResponse, bool repoResponse, bool? expected, Type? exceptionType)
    {
        Mock<IAddressRepo> repo = new Mock<IAddressRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        userRepo.Setup(userRepo => userRepo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(foundResponse);
        repo.Setup(repo => repo.DeleteByIdAsync(It.IsAny<Address>())).Returns(Task.FromResult(repoResponse));
        AddressService service = new AddressService(repo.Object, userRepo.Object, GetMapper());

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

    [Theory]
    [ClassData(typeof(UserAddressData))]
    public async void GetAddreess_ShouldReturn_ValidResponse(Address response, AddressReadDTO expected)
    {
        Mock<IAddressRepo> repo = new Mock<IAddressRepo>();
        Mock<IUserRepo> userRepo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.GetAddreess(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        AddressService service = new AddressService(repo.Object, userRepo.Object, GetMapper());

        AddressReadDTO result = await service.GetAddreess(It.IsAny<Guid>());
        Assert.Equivalent(expected, result);
    }

    public class UserAddressData : TheoryData<Address?, AddressReadDTO>
    {
        public UserAddressData()
        {
            Address address = new Address() { City = "Some city", Country = "Some country", PostCode = "12345", Street = "Street 3", HouseNumber = "A1", UserId = new Guid() };
            Add(address, GetMapper().Map<Address, AddressReadDTO>(address));
        }
    }

    public class DeleteAddressData : TheoryData<User?, Address?, bool, bool?, Type?>
    {
        public DeleteAddressData()
        {
            PasswordService.HashPassword("12345", out string hashedPassword, out byte[] salt);
            User user = new User() { Name = "John Doe", Email = "john@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
            Address address = new Address()
            {
                HouseNumber = "A1",
                City = "Some city",
                Country = "Some country",
                PostCode = "12345",
                Street = "Street 1",
                UserId = new Guid()
            };
            user.Address = address;
            User user1 = new User() { Name = "John1 Doe", Email = "john1@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
            Add(user, address, true, true, null);
            Add(user, null, false, null, typeof(CustomException));
            Add(null, null, false, null, typeof(CustomException));
            Add(user1, null, false, null, typeof(CustomException));
        }
    }

    public class UpdateOneAddressData : TheoryData<User?, AddressUpdateDTO?, Address?, Address?, AddressReadDTO?, Type?>
    {
        public UpdateOneAddressData()
        {
            PasswordService.HashPassword("12345", out string hashedPassword, out byte[] salt);
            User user = new User() { Name = "John Doe", Email = "john@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
            User user1 = new User() { Name = "John1 Doe", Email = "john1@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
            Address address = new Address()
            {
                HouseNumber = "A1",
                City = "Some city",
                Country = "Some country",
                PostCode = "12345",
                Street = "Street 1",
                UserId = new Guid()
            };

            user.Address = address;

            AddressUpdateDTO addressInput = new AddressUpdateDTO()
            {
                HouseNumber = "A7",
                City = "Some city",
                Country = "Some country",
                PostCode = "12345",
                Street = "Street 1"
            };
            Address updatedaddress = new Address()
            {
                HouseNumber = "A7",
                City = "Some city",
                Country = "Some country",
                PostCode = "12345",
                Street = "Street 1",
                UserId = new Guid()
            };
            Add(user, addressInput, address, address, GetMapper().Map<Address, AddressReadDTO>(updatedaddress), null);
            Add(user, addressInput, null, null, null, typeof(CustomException));
            Add(user1, addressInput, null, null, null, typeof(CustomException));
            Add(null, addressInput, null, null, null, typeof(CustomException));
        }
    }

    public class CreateOneAddressData : TheoryData<AddressCreateDTO, Address, AddressReadDTO>
    {
        public CreateOneAddressData()
        {
            AddressCreateDTO addressInput = new AddressCreateDTO() { City = "Some city", Country = "Some country", PostCode = "12345", Street = "Street 1" };
            Address address = GetMapper().Map<AddressCreateDTO, Address>(addressInput);
            Add(addressInput, address, GetMapper().Map<Address, AddressReadDTO>(address));
        }
    }

    public class GetOneByIdData : TheoryData<Address, AddressReadDTO, Type>
    {
        public GetOneByIdData()
        {
            Address address = new Address() { City = "Some city", Country = "Some country", PostCode = "12345", Street = "Street 3", HouseNumber = "A1", UserId = new Guid() };
            Add(address, GetMapper().Map<Address, AddressReadDTO>(address), null);
            Add(null, null, typeof(CustomException));
        }
    }

    public class GetAllAddressesData : TheoryData<IEnumerable<Address>, IEnumerable<AddressReadDTO>>
    {
        public GetAllAddressesData()
        {
            Address address = new Address() { City = "Some city", Country = "Some country", PostCode = "12345", Street = "Street 3", HouseNumber = "A1", UserId = new Guid() };
            IEnumerable<Address> addresses = new List<Address>() { address };
            Add(addresses, GetMapper().Map<IEnumerable<Address>, IEnumerable<AddressReadDTO>>(addresses));
        }
    }
}