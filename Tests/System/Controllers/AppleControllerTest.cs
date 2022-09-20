using Microsoft.AspNetCore.Mvc;

using FluentAssertions;
using Moq;

using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Services;

using Tests.Fixtures;

namespace Tests.System.Controllers
{
    public class AppleControllerTest
    {
        [Fact]
        public async void GetById_ExistingIdPassed_ReturnsOkObject()
        {
            int id = 2;
            var mock = new Mock<IAppleService>();
            mock.Setup(service => service.FindById(id)).ReturnsAsync(AppleFixture.GetTestApples());

            var controller = new AppleController(mock.Object);
            var result = (OkObjectResult)await controller.FindById(id);

            result.StatusCode.Should().Be(200);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetById_NotExistingIdPassed_ReturnsNotFound()
        {
            int id = 7;
            var mock = new Mock<IAppleService>();
            var response = new GeoserverResponse<Apple>() { features = new List<Feature<Apple>>() };
            mock.Setup(service => service.FindById(id)).ReturnsAsync(response);

            var controller = new AppleController(mock.Object);
            var result = (NotFoundResult)await controller.FindById(id);

            result.StatusCode.Should().Be(404);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

