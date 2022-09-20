using Microsoft.AspNetCore.Mvc;

using FluentAssertions;
using Moq;

using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Services;

using Tests.Fixtures;

namespace Tests.System.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public async void Get_OnSuccess_Returns200()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(service => service.GetAll()).ReturnsAsync(UserFixture.GetTestUsers());

            var controller = new UserController(mock.Object);
            var result = (OkObjectResult)await controller.GetAll();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void Get_OnSuccess_InvokesUserService()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(service => service.GetAll()).ReturnsAsync(new List<User>());
            var controller = new UserController(mock.Object);

            var result = await controller.GetAll();

            mock.Verify(service => service.GetAll(), Times.Once());
        }

        [Fact]
        public async void Get_OnSuccess_ReturnsListOfUsers()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(service => service.GetAll()).ReturnsAsync(UserFixture.GetTestUsers());
            var controller = new UserController(mock.Object);

            var result = await controller.GetAll();

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async void Get_OnNoUsersFound_Returns404()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(service => service.GetAll()).ReturnsAsync(new List<User>());
            var controller = new UserController(mock.Object);

            var result = await controller.GetAll();

            result.Should().BeOfType<NotFoundObjectResult>();
            var objectResult = (NotFoundObjectResult)result;
            objectResult.StatusCode.Should().Be(404);
        }
    }
}

