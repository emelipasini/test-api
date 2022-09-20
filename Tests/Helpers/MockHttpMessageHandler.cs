using Http = System.Net.Http;
using System.Net;

using Newtonsoft.Json;
using Moq;
using Moq.Protected;
using WebAPI.Models;

namespace Tests.Helpers
{
    internal static class MockHttpMessageHandler<T>
    {
        internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse)
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };
            var mediaType = new Http.Headers.MediaTypeHeaderValue("application/json");
            mockResponse.Content.Headers.ContentType = mediaType;

            var hanlderMock = new Mock<HttpMessageHandler>();
            hanlderMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(mockResponse);

            return hanlderMock;
        }

        internal static Mock<HttpMessageHandler> SetupBasicGetGeoserverResourceList(GeoserverResponse<T> expectedResponse)
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };
            var mediaType = new Http.Headers.MediaTypeHeaderValue("application/json");
            mockResponse.Content.Headers.ContentType = mediaType;

            var hanlderMock = new Mock<HttpMessageHandler>();
            hanlderMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(mockResponse);

            return hanlderMock;
        }

        internal static Mock<HttpMessageHandler> SetupReturns404(GeoserverResponse<T> expectedResponse)
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };
            var mediaType = new Http.Headers.MediaTypeHeaderValue("application/json");
            mockResponse.Content.Headers.ContentType = mediaType;

            var hanlderMock = new Mock<HttpMessageHandler>();
            hanlderMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(mockResponse);

            return hanlderMock;
        }
        
        internal static Mock<HttpMessageHandler> SetupReturns404()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("")
            };
            var mediaType = new Http.Headers.MediaTypeHeaderValue("application/json");
            mockResponse.Content.Headers.ContentType = mediaType;

            var hanlderMock = new Mock<HttpMessageHandler>();
            hanlderMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(mockResponse);

            return hanlderMock;
        }
    }
}

