using Moq;
using Moq.Protected;

using WebAPI.Models;
using WebAPI.Services;

using Tests.Fixtures;
using Tests.Helpers;
using FluentAssertions;

namespace Tests.System.Services
{
    public class UserServiceTest
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeHttpRequest()
        {
            // Arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var service = new UserService(httpClient);

            // Act
            await service.GetAll();

            // Assert
            handlerMock.Protected().Verify(
                "SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            // Arrange
            var handlerMock = MockHttpMessageHandler<User>.SetupReturns404();
            var httpClient = new HttpClient(handlerMock.Object);
            var service = new UserService(httpClient);

            // Act
            var result = await service.GetAll();

            // Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            // Arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var service = new UserService(httpClient);

            // Act
            var result = await service.GetAll();

            // Assert
            result.Count.Should().Be(expectedResponse.Count);
        }
    }
}
