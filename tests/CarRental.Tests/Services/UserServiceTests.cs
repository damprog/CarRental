using CarRental.Core.Domain;
using CarRental.Core.Repositories;
using CarRental.Infrastructure.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_add_async_on_user_repository()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            var userService = new UserService(userRepositoryMock.Object, jwtHandlerMock.Object);

            //Act
            await userService.RegisterAsync(Guid.NewGuid(), "emial", "name", "password");

            //Assert
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Core.Domain.User>()), Times.Once());

        }

        [Fact]
        public async Task when_invoking_get_async_it_should_invoke_get_async_on_user_repository()
        {
            //Arrange
            User user = new User(Guid.NewGuid(), "user", "firstname and surname", "email@kkk.kl", "password");
            var userRepositoryMock = new Mock<IUserRepository>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            var userService = new UserService(userRepositoryMock.Object, jwtHandlerMock.Object);

            userRepositoryMock.Setup(x => x.GetAsync(user.Id)).ReturnsAsync(user);

            //Act
            var accountDto = await userService.GetAccountAsync(user.Id);

            //Assert
            userRepositoryMock.Verify(x => x.GetAsync(user.Id), Times.Once());
            accountDto.Should().NotBeNull();
            accountDto.Email.Should().BeEquivalentTo(user.Email);

        }
    }
}
