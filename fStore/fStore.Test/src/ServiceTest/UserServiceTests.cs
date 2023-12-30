using AutoMapper;
using fStore.Business;
using fStore.Core;
using Moq;

namespace fStore.Test;
public class UserServiceTests
{
    public UserServiceTests()
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

    [Fact]
    public async void GetAllAsync_ShouldInvoke_RepoMethod()
    {
        var repo = new Mock<IUserRepo>();
        var mapper = new Mock<IMapper>();
        var userService = new UserService(repo.Object, GetMapper());
        //GetAllParams options = new GetAllParams() { Limit = 10, Offset = 0 };
        GetAllParams options = new GetAllParams() { };
        await userService.GetAllAsync(options);

        repo.Verify(repo => repo.GetAllAsync(options), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetAllUsersData))]
    public async void GetAllAsync_ShouldReturn_Response_ListOfUsers(IEnumerable<User> repoResponse, IEnumerable<UserReadDTO> expected)
    {
        var repo = new Mock<IUserRepo>();
        //GetAllParams options = new GetAllParams() { Limit = 10, Offset = 0 };
        GetAllParams options = new GetAllParams() { };
        repo.Setup(repo => repo.GetAllAsync(options)).Returns(Task.FromResult(repoResponse));
        var userService = new UserService(repo.Object, GetMapper());

        var response = await userService.GetAllAsync(options);

        Assert.Equivalent(expected, response);
    }

    [Fact]
    public async void GetUserById_ShouldInvoke_RepoMethod()
    {
        var repo = new Mock<IUserRepo>();
        PasswordService.HashPassword("12345", out string hashedPassword, out byte[] salt);
        User user1 = new User() { Name = "John Doe", Email = "john@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user1);
        var mapper = new Mock<IMapper>();
        var userService = new UserService(repo.Object, GetMapper());

        await userService.GetByIdAsync(It.IsAny<Guid>());

        repo.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetOneUserData))]
    public async void GetUserById_ShouldReturn_ValidResponse(User? repoResponse, UserReadDTO? expected, Type? exceptionType)
    {
        var repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(repoResponse));
        var userService = new UserService(repo.Object, GetMapper());

        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => userService.GetByIdAsync(It.IsAny<Guid>()));
        }
        else
        {
            var response = await userService.GetByIdAsync(It.IsAny<Guid>());

            Assert.Equivalent(expected, response);
        }
    }

    [Fact]
    public async void CreateOneAsync_ShouldInvoke_RepoMethod()
    {
        var repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.IsEmailAvailableAsync(It.IsAny<string>())).ReturnsAsync(false);
        var mapper = new Mock<IMapper>();
        var userService = new UserService(repo.Object, GetMapper());
        var dto = new UserCreateDTO { Name = "John Doe", Email = "john@example.com", Password = "12345", Avatar = "https://picsum.photos/200" };

        await userService.CreateOneAsync(new Guid(), dto);

        repo.Verify(repo => repo.CreateOneAsync(It.IsAny<User>()), Times.Once);
    }

    [Theory]
    [ClassData(typeof(CreateUserData))]
    public async void CreateOneAsync_ShouldReturn_ValidResponse(bool emailAvailableResponse, User repoResponse, UserReadDTO expected, Type? exceptionType)
    {
        var repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.CreateOneAsync(It.IsAny<User>())).ReturnsAsync(repoResponse);
        repo.Setup(repo => repo.IsEmailAvailableAsync(It.IsAny<string>())).ReturnsAsync(emailAvailableResponse);
        var userService = new UserService(repo.Object, GetMapper());
        var dto = new UserCreateDTO { Name = "John Doe", Email = "john1@mail.com", Password = "12345", Avatar = "https://picsum.photos/200" };
        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => userService.CreateOneAsync(new Guid(), dto));
        }
        else
        {
            var response = await userService.CreateOneAsync(new Guid(), dto);

            Assert.Equivalent(expected, response);
        }
    }

    [Fact]
    public async void DeleteUserAsync_ShouldInvoke_RepoMethod()
    {
        var repo = new Mock<IUserRepo>();
        PasswordService.HashPassword("12345", out string hashedPassword, out byte[] salt);
        User user1 = new User() { Name = "John Doe", Email = "john@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user1);
        var mapper = new Mock<IMapper>();
        var userService = new UserService(repo.Object, GetMapper());

        await userService.DeleteByIdAsync(It.IsAny<Guid>());

        repo.Verify(repo => repo.DeleteByIdAsync(It.IsAny<User>()), Times.Once);
    }

    [Theory]
    [ClassData(typeof(DeleteUserData))]
    public async void DeleteUserAsync_ShouldReturn_ValidResponse(User? foundResponse, bool repoResponse, bool? expected, Type? exceptionType)
    {
        var repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(foundResponse);
        repo.Setup(repo => repo.DeleteByIdAsync(It.IsAny<User>())).ReturnsAsync(repoResponse);
        var userService = new UserService(repo.Object, GetMapper());

        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => userService.DeleteByIdAsync(It.IsAny<Guid>()));
        }
        else
        {
            var response = await userService.DeleteByIdAsync(It.IsAny<Guid>());

            Assert.Equivalent(expected, response);
        }
    }

    [Fact]
    public async void UpdateUser_ShouldInvoke_RepoMethod()
    {
        var repo = new Mock<IUserRepo>();
        var mockService = new Mock<IUserService>();
        PasswordService.HashPassword("12345", out string hashedPassword, out byte[] salt);
        User user = new User() { Name = "John Doe", Email = "john@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
        repo.Setup(repo => repo.IsEmailAvailableAsync(It.IsAny<string>())).ReturnsAsync(false);
        var userUpdateDTO = new UserUpdateDTO { Name = "John1 Doe", Email = "john1@example.com", Avatar = "https://i.imgur.com/LDOO4Qs.jpg" };
        var mapper = new Mock<IMapper>();
        var userService = new UserService(repo.Object, GetMapper());
        await userService.UpdateOneAsync(It.IsAny<Guid>(), userUpdateDTO);

        repo.Verify(repo => repo.IsEmailAvailableAsync(It.IsAny<string>()), Times.Once);
        repo.Verify(repo => repo.UpdateOneAsync(It.IsAny<Guid>(), It.IsAny<User>()), Times.Once);
    }

    [Theory]
    [ClassData(typeof(UpdateUserData))]
    public async void UpdateUserRoleAsync_ShouldReturn_ValidResponse(bool emailAvailableResponse, UserUpdateDTO? userUpdateDTO, User? foundResponse, User repoResponse, UserReadDTO? expected, Type? exceptionType)
    {
        var repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(foundResponse);
        repo.Setup(repo => repo.IsEmailAvailableAsync(It.IsAny<string>())).ReturnsAsync(emailAvailableResponse);
        repo.Setup(repo => repo.UpdateOneAsync(It.IsAny<Guid>(), It.IsAny<User>())).ReturnsAsync(repoResponse);
        var userService = new UserService(repo.Object, GetMapper());

        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => userService.UpdateOneAsync(It.IsAny<Guid>(), userUpdateDTO));
        }
        else
        {
            var response = await userService.UpdateOneAsync(It.IsAny<Guid>(), userUpdateDTO);

            Assert.Equivalent(expected, response);
        }
    }

    [Fact]
    public async void EmailAvailableAsync_ShouldInvoke_RepoMethod()
    {
        var repo = new Mock<IUserRepo>();
        var mapper = new Mock<IMapper>();
        var userService = new UserService(repo.Object, GetMapper());

        await userService.IsEmailAvailable(It.IsAny<string>());

        repo.Verify(repo => repo.IsEmailAvailableAsync(It.IsAny<string>()), Times.Once);
    }

    [Theory]
    [ClassData(typeof(EmailAvailableData))]
    public async void EmailAvailableAsync_ShouldReturn_ValidResponse(bool repoResponse, bool expected, Type? exceptionType)
    {
        var repo = new Mock<IUserRepo>();
        repo.Setup(repo => repo.IsEmailAvailableAsync(It.IsAny<string>())).ReturnsAsync(repoResponse);
        var userService = new UserService(repo.Object, GetMapper());

        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => userService.IsEmailAvailable(It.IsAny<string>()));
        }
        else
        {
            var response = await userService.IsEmailAvailable(It.IsAny<string>());

            Assert.Equivalent(expected, response);
        }
    }

    public class EmailAvailableData : TheoryData<bool, bool, Type?>
    {
        public EmailAvailableData()
        {
            Add(true, true, null);
            Add(false, false, null);
        }
    }

    public class UpdateUserData : TheoryData<bool, UserUpdateDTO?, User, User, UserReadDTO, Type?>
    {
        public UpdateUserData()
        {
            PasswordService.HashPassword("12345", out string hashedPassword, out byte[] salt);
            User user = new User() { Name = "John Doe", Email = "john@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
            User updatedUser = new User() { Name = "John1 Doe", Email = "john@example.com", Password = hashedPassword, Avatar = "https://i.imgur.com/LDOO4Qs.jpg", Salt = salt, Role = Role.Customer };
            User updatedUser1 = new User() { Name = "John Doe", Email = "john1@example.com", Password = hashedPassword, Avatar = "https://i.imgur.com/LDOO4Qs.jpg", Salt = salt, Role = Role.Customer };
            var userUpdateDTO = new UserUpdateDTO { Name = "John1 Doe", Avatar = "https://i.imgur.com/LDOO4Qs.jpg" };
            var userUpdateDTO1 = new UserUpdateDTO { Name = "John Doe", Email = "john1@example.com", Avatar = "https://i.imgur.com/LDOO4Qs.jpg" };
            var userUpdateDTO2 = new UserUpdateDTO { Name = "John1 Doe", Email = "john@example.com", Avatar = "https://i.imgur.com/LDOO4Qs.jpg" };
            UserReadDTO customerReadDto = GetMapper().Map<User, UserReadDTO>(updatedUser);
            UserReadDTO customerReadDto1 = GetMapper().Map<User, UserReadDTO>(updatedUser1);
            Add(false, userUpdateDTO, user, updatedUser, customerReadDto, null);
            Add(false, userUpdateDTO1, user, updatedUser1, customerReadDto1, null);
            Add(false, null, null, null, null, typeof(CustomException));
            Add(true, userUpdateDTO2, user, null, null, typeof(CustomException));
        }
    }

    public class GetAllUsersData : TheoryData<IEnumerable<User>, IEnumerable<UserReadDTO>>
    {
        public GetAllUsersData()
        {
            PasswordService.HashPassword("12345", out string hashedPassword, out byte[] salt);
            User user1 = new User() { Name = "John Doe", Email = "john@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
            User user2 = new User() { Name = "Jane Doe", Email = "jane@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
            User user3 = new User() { Name = "Jack Doe", Email = "jack@example.com", Password = "12345", Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
            IEnumerable<User> users = new List<User>() { user1, user2, user3 };
            Add(users, GetMapper().Map<IEnumerable<User>, IEnumerable<UserReadDTO>>(users));
        }
    }

    public class GetOneUserData : TheoryData<User?, UserReadDTO?, Type?>
    {
        public GetOneUserData()
        {
            PasswordService.HashPassword("12345", out string hashedPassword, out byte[] salt);
            User user = new User() { Name = "John Doe", Email = "john@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
            UserReadDTO userReadDto = GetMapper().Map<User, UserReadDTO>(user);
            Add(user, userReadDto, null);
            Add(null, null, typeof(CustomException));
        }
    }

    public class CreateUserData : TheoryData<bool, User?, UserReadDTO?, Type?>
    {
        public CreateUserData()
        {
            PasswordService.HashPassword("12345", out string hashedPassword, out byte[] salt);
            User user = new User() { Name = "John Doe", Email = "john@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
            UserReadDTO userReadDto = GetMapper().Map<User, UserReadDTO>(user);
            Add(false, user, userReadDto, null);
            Add(true, null, null, typeof(CustomException));
        }
    }
    public class DeleteUserData : TheoryData<User?, bool, bool?, Type?>
    {
        public DeleteUserData()
        {
            PasswordService.HashPassword("12345", out string hashedPassword, out byte[] salt);
            User user = new User() { Name = "John Doe", Email = "john@example.com", Password = hashedPassword, Avatar = "https://picsum.photos/200", Role = Role.Customer, Salt = salt };
            Add(user, true, true, null);
            Add(null, false, null, typeof(CustomException));
        }
    }
}
