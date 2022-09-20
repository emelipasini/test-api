using FluentAssertions;
using Moq;
using Moq.Protected;

using WebAPI.Models;
using WebAPI.Services;

using Tests.Fixtures;
using Tests.Helpers;

namespace Tests.System.Services
{
    public class UserServiceTest
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeHttpRequest()
        {
            var expectedResponse = UserFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var service = new UserService(httpClient);
            await service.GetAll();

            handlerMock.Protected().Verify(
                "SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            var handlerMock = MockHttpMessageHandler<User>.SetupReturns404();
            var httpClient = new HttpClient(handlerMock.Object);

            var service = new UserService(httpClient);
            var result = await service.GetAll();

            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            var expectedResponse = UserFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var service = new UserService(httpClient);
            var result = await service.GetAll();

            result.Count.Should().Be(expectedResponse.Count);
        }
    }
}

