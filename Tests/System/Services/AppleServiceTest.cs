using Microsoft.Extensions.Configuration;

using FluentAssertions;

using WebAPI.Models;
using WebAPI.Services;

using Tests.Fixtures;
using Tests.Helpers;

namespace Tests.System.Services
{

    public class AppleServiceTest
    {
        private readonly IConfiguration _configuration;

        public AppleServiceTest()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("testsettings.json", optional: false).Build();
        }

        [Fact]
        public async Task GetById_WhenHits404_ReturnsEmptyObject()
        {
            var expectedResponse = new GeoserverResponse<Apple>();
            var handlerMock = MockHttpMessageHandler<Apple>.SetupReturns404(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var service = new AppleService(_configuration, httpClient);

            var result = await service.FindById(1);

            result.Equals(null);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsExpectedApple()
        {
            int id = 5;
            var expectedResponse = AppleFixture.GetTestAppleById();
            var handlerMock = MockHttpMessageHandler<Apple>.SetupBasicGetGeoserverResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var service = new AppleService(_configuration, httpClient);

            var result = await service.FindById(id);

            result.features.Count.Should().Be(1);
            Assert.IsType<GeoserverResponse<Apple>>(result);
            Assert.Equal(id, result.features.First().properties.id);
        }
    }
}

