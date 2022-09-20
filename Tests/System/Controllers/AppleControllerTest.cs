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
            var result = (NotFoundObjectResult)await controller.FindById(id);

            result.StatusCode.Should().Be(404);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        
        [Fact]
        public async void GetById_OnSuccess_InvokesAppleService()
        {
            int id = 1;
            var mock = new Mock<IAppleService>();
            mock.Setup(service => service.FindById(id)).ReturnsAsync(new GeoserverResponse<Apple>());
            var controller = new AppleController(mock.Object);

            var result = await controller.FindById(id);

            mock.Verify(service => service.FindById(id), Times.Once());
        }

        [Fact]
        public async void GetByDate_ValidDatePassed_ReturnsOkObject()
        {
            string date = "2022-09-14";
            var mock = new Mock<IAppleService>();
            mock.Setup(service => service.FindByDate(date)).ReturnsAsync(AppleFixture.GetTestApples());

            var controller = new AppleController(mock.Object);
            var result = (OkObjectResult)await controller.FindByDate(date);

            result.StatusCode.Should().Be(200);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetByDate_InvalidDatePassed_ReturnsNotFound()
        {
            string date = "20-09-2022";
            var mock = new Mock<IAppleService>();
            var response = new GeoserverResponse<Apple>() { features = new List<Feature<Apple>>() };
            mock.Setup(service => service.FindByDate(date)).ReturnsAsync(response);

            var controller = new AppleController(mock.Object);
            var result = (NotFoundObjectResult)await controller.FindByDate(date);

            result.StatusCode.Should().Be(404);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        
        [Fact]
        public async void GetByDate_OnSuccess_InvokesAppleService()
        {
            string date = "2022-09-14";
            var mock = new Mock<IAppleService>();
            mock.Setup(service => service.FindByDate(date)).ReturnsAsync(new GeoserverResponse<Apple>());
            var controller = new AppleController(mock.Object);

            var result = await controller.FindByDate(date);

            mock.Verify(service => service.FindByDate(date), Times.Once());
        }
    }
}

