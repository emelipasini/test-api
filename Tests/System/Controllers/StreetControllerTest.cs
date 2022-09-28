using Microsoft.AspNetCore.Mvc;

using FluentAssertions;
using Moq;

using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Services;

using Tests.Fixtures;

namespace Tests.System.Controllers
{
    public class StreetControllerTest
    {
        [Fact]
        public async void GetAll_OnSuccess_Returns200()
        {
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.GetAll()).ReturnsAsync(StreetFixture.GetTestStreets());

            var controller = new StreetController(mock.Object);
            var result = (OkObjectResult)await controller.GetAll();

            result.StatusCode.Should().Be(200);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAll_OnEmptyList_Returns204()
        {
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.GetAll()).ReturnsAsync(new List<Street>());

            var controller = new StreetController(mock.Object);
            var result = (NoContentResult)await controller.GetAll();

            result.StatusCode.Should().Be(204);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void GetAll_OnSuccess_InvokesService()
        {
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.GetAll()).ReturnsAsync(new List<Street>());

            var controller = new StreetController(mock.Object);
            var result = await controller.GetAll();

            mock.Verify(service => service.GetAll(), Times.Once());
        }

        [Fact]
        public async void GetById_ValidIdPassed_Returns200()
        {
            int id = 2;
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.GetById(id)).ReturnsAsync(StreetFixture.GetTestStreetById());

            var controller = new StreetController(mock.Object);
            var result = (OkObjectResult)await controller.GetById(id);

            result.StatusCode.Should().Be(200);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetById_InvalidIdPassed_Returns400()
        {
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.GetById(0));

            var controller = new StreetController(mock.Object);
            var result = (BadRequestObjectResult)await controller.GetById(0);

            result.StatusCode.Should().Be(400);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void GetById_NotExistingIdPassed_Returns404()
        {
            int id = 7;
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.GetById(id)).ReturnsAsync(new Street());

            var controller = new StreetController(mock.Object);
            var result = (NotFoundObjectResult)await controller.GetById(id);

            result.StatusCode.Should().Be(404);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async void GetById_OnSuccess_InvokesService()
        {
            int id = 1;
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.GetById(id)).ReturnsAsync(new Street());

            var controller = new StreetController(mock.Object);
            var result = await controller.GetById(id);

            mock.Verify(service => service.GetById(id), Times.Once());
        }

        [Fact]
        public async void Create_ValidArgumentPassed_Returns200()
        {
            var mock = new Mock<IStreetService>();
            var street = StreetFixture.GetTestStreetById();
            mock.Setup(service => service.Create(street)).ReturnsAsync(street);

            var controller = new StreetController(mock.Object);
            var result = (OkObjectResult)await controller.Create(street);

            result.StatusCode.Should().Be(200);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Create_InvalidArgumentPassed_Returns400()
        {
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.Create(null));

            var controller = new StreetController(mock.Object);
            var result = (BadRequestObjectResult)await controller.Create(null);

            result.StatusCode.Should().Be(400);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Create_OnSuccess_InvokesService()
        {
            var mock = new Mock<IStreetService>();
            var street = StreetFixture.GetTestStreetById();
            mock.Setup(service => service.Create(street)).ReturnsAsync(street);

            var controller = new StreetController(mock.Object);
            var result = await controller.Create(street);

            mock.Verify(service => service.Create(street), Times.Once());
        }

        [Fact]
        public async void Update_ValidArgumentPassed_Returns200()
        {
            var mock = new Mock<IStreetService>();
            var street = StreetFixture.GetTestStreetById();
            mock.Setup(service => service.Update(street)).ReturnsAsync(street);

            var controller = new StreetController(mock.Object);
            var result = (OkObjectResult)await controller.Update(street);

            result.StatusCode.Should().Be(200);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Update_InvalidArgumentPassed_Returns400()
        {
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.Update(null));

            var controller = new StreetController(mock.Object);
            var result = (BadRequestObjectResult)await controller.Update(null);

            result.StatusCode.Should().Be(400);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Update_NotExistingArgumentPassed_Returns404()
        {
            var mock = new Mock<IStreetService>();
            var street = StreetFixture.GetTestStreetById();
            mock.Setup(service => service.Update(street)).ReturnsAsync(new Street());

            var controller = new StreetController(mock.Object);
            var result = (NotFoundObjectResult)await controller.Update(street);

            result.StatusCode.Should().Be(404);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async void Update_OnSuccess_InvokesService()
        {
            var mock = new Mock<IStreetService>();
            var street = StreetFixture.GetTestStreetById();
            mock.Setup(service => service.Update(street)).ReturnsAsync(street);

            var controller = new StreetController(mock.Object);
            var result = await controller.Update(street);

            mock.Verify(service => service.Update(street), Times.Once());
        }

        [Fact]
        public async void Delete_ValidIdPassed_Returns200()
        {
            int id = 2;
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.Delete(id)).ReturnsAsync(true);

            var controller = new StreetController(mock.Object);
            var result = (OkObjectResult)await controller.Delete(id);

            result.StatusCode.Should().Be(200);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Delete_NotExistingIdPassed_Returns404()
        {
            int id = 7;
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.Delete(id)).ReturnsAsync(false);

            var controller = new StreetController(mock.Object);
            var result = (NotFoundObjectResult)await controller.Delete(id);

            result.StatusCode.Should().Be(404);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async void Delete_OnSuccess_InvokesService()
        {
            int id = 2;
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.Delete(id)).ReturnsAsync(true);

            var controller = new StreetController(mock.Object);
            var result = await controller.Delete(id);

            mock.Verify(service => service.Delete(id), Times.Once());
        }

        [Fact]
        public async void Delete_InvalidIdPassed_Returns400()
        {
            var mock = new Mock<IStreetService>();
            mock.Setup(service => service.Update(null));

            var controller = new StreetController(mock.Object);
            var result = (BadRequestObjectResult)await controller.Delete(0);

            result.StatusCode.Should().Be(400);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}

