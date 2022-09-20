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

        [Fact]
        public async Task GetById_WhenHits404_ReturnsEmptyObject()
        {
            int id = 1;
            var expectedResponse = new GeoserverResponse<Apple>();
            var handlerMock = MockHttpMessageHandler<Apple>.SetupReturns404(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var service = new AppleService(_configuration, httpClient);
            var result = await service.FindById(id);

            result.features.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetByDate_WhenCalled_ReturnsExpectedApples()
        {
            string date = "2022-09-14";
            var expectedResponse = AppleFixture.GetTestApples();
            var handlerMock = MockHttpMessageHandler<Apple>.SetupBasicGetGeoserverResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var service = new AppleService(_configuration, httpClient);
            var result = await service.FindByDate(date);

            result.features.Count.Should().Be(expectedResponse.features.Count);
            Assert.IsType<GeoserverResponse<Apple>>(result);
            Assert.Equal(1, result.features.First().properties.id);
        }

        [Fact]
        public async Task GetByDate_WhenHits404_ReturnsEmptyObject()
        {
            string date = "2022-09-14";
            var expectedResponse = new GeoserverResponse<Apple>();
            var handlerMock = MockHttpMessageHandler<Apple>.SetupReturns404(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var service = new AppleService(_configuration, httpClient);
            var result = await service.FindByDate(date);

            result.features.Count.Should().Be(0);
        }
    }
}

