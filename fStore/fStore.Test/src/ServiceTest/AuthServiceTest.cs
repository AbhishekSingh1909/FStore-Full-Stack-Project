using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using fStore.Business;
using fStore.Core;
using Moq;
using Xunit;

namespace fStore.Test;

public class AuthServiceTest
{
    public AuthServiceTest()
    {

    }

    [Fact]
    public async void LoginAsync_ShouldInvoke_RepoMethod()
    {
        var repo = new Mock<IUserRepo>();
        var tokenServiceMock = new Mock<ITokenService>();
        var validcredentials = new LoginParams { Email = "user@example.com", Password = "password" };
        PasswordService.HashPassword("password", out string hashedPassword, out byte[] salt);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Jone Doe",
            Email = "user1@example.com",
            Avatar = "avatarURL",
            Password = hashedPassword,
            Salt = salt,
            Role = Role.Customer
        };
        var authService = new AuthService(repo.Object, tokenServiceMock.Object);
        repo.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        await authService.LoginAsync(validcredentials);

        repo.Verify(repo => repo.FindByEmailAsync(It.IsAny<string>()), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetUserCredentialsData))]
    public async Task LoginAsync_ValidCredentials_ReturnsToken(LoginParams credentials, User? foundUser, User? repoResponse, Type? exceptionType)
    {
        var repo = new Mock<IUserRepo>();
        repo
          .Setup(repo => repo.FindByEmailAsync(credentials.Email))
          .ReturnsAsync(foundUser);

        var tokenServiceMock = new Mock<ITokenService>();
        tokenServiceMock.Setup(tokenService => tokenService.GenerateToken(repoResponse))
            .Returns("Bearer Token");

        var authService = new AuthService(repo.Object, tokenServiceMock.Object);

        if (exceptionType is not null)
        {
            await Assert.ThrowsAsync(exceptionType, () => authService.LoginAsync(credentials));
        }
        else
        {
            // Act
            var result = await authService.LoginAsync(credentials);

            // Assert
            Assert.Equal("Bearer Token", result);
        }
    }

    public class GetUserCredentialsData : TheoryData<LoginParams, User?,User?,Type?>
    {
        public GetUserCredentialsData()
        {
            var validcredentials = new LoginParams { Email = "user@example.com", Password = "password" };
            var wrongEmail = new LoginParams { Email = "user1@example.com", Password = "password" };
            var wrongpassword = new LoginParams { Email = "user@example.com", Password = "password1" };
            PasswordService.HashPassword("password", out string hashedPassword, out byte[] salt);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Jone Doe",
                Email = "user1@example.com",
                Avatar = "avatarURL",
                Password = hashedPassword,
                Salt = salt,
                Role = Role.Customer
            };
            Add(validcredentials,user, user, null);
            Add(wrongEmail, null, null, typeof(CustomException));
            Add(wrongpassword, user, user, typeof(CustomException));
        }
    }
}