using AutoFixture;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MailerQ.RestApi.Test
{
    public class RestApiClientTest
    {
        private const string Version = "v1";
        private static readonly Fixture specimens = new();

        private static IRestApiClient CreateSut(
            MailerQConfiguration mailerQConfiguration = null,
            IHttpClientFactory httpClientFactory = null
            )
        {
            mailerQConfiguration ??= new MailerQConfiguration { RestApiUrl = $"http://localhost/{Version}/", RestApiToken = specimens.Create<string>() };

            return new RestApiClient(
                options: Options.Create(mailerQConfiguration),
                httpFactory: httpClientFactory ?? Mock.Of<IHttpClientFactory>()
                );
        }

        private static Mock<HttpMessageHandler> CreateHttpMessageHandlerMock(HttpResponseMessage httpResponseMessage)
        {
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(nameof(HttpClient.SendAsync), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            return httpMessageHandlerMock;
        }

        private static Mock<HttpMessageHandler> CreateHttpMessageHandlerMock(Exception exception)
        {
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(nameof(HttpClient.SendAsync), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(exception);

            return httpMessageHandlerMock;
        }

        private static Mock<IHttpClientFactory> CreateHttpClientFactoryMock(HttpMessageHandler httpMessageHandler)
        {
            var httpClient = new HttpClient(httpMessageHandler);

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            return httpClientFactoryMock;
        }

        public static IEnumerable<object[]> RestApiModelSpecimens()
        {
            yield return new object[] { specimens.Create<Inject>() };
            yield return new object[] { specimens.Create<Pause>() };
            yield return new object[] { specimens.Create<Error>() };
            yield return new object[] { specimens.Create<Pool>() };
            yield return new object[] { specimens.Create<IpPool>() };
            yield return new object[] { specimens.Create<PoolIP>() };
            yield return new object[] { specimens.Create<IpAddress>() };
            yield return new object[] { specimens.Create<Suppression>() };
            yield return new object[] { specimens.Create<ExternalMTA>() };
            yield return new object[] { specimens.Create<ExternalIp>() };
        }

        public static IEnumerable<object[]> RestApiModelEndpointSpecimens()
        {
            yield return new object[] { specimens.Create<Inject>(), $"{Version}/inject" };
            yield return new object[] { specimens.Create<Pause>(), $"{Version}/pauses" };
            yield return new object[] { specimens.Create<Error>(), $"{Version}/errors" };
            yield return new object[] { specimens.Create<Pool>(), $"{Version}/pools" };
            yield return new object[] { specimens.Create<IpPool>(), $"{Version}/pools" };
            yield return new object[] { specimens.Create<PoolIP>(), $"{Version}/poolips" };
            yield return new object[] { specimens.Create<IpAddress>(), $"{Version}/poolips" };
            yield return new object[] { specimens.Create<Suppression>(), $"{Version}/suppressions" };
            yield return new object[] { specimens.Create<ExternalMTA>(), $"{Version}/externalmtas" };
            yield return new object[] { specimens.Create<ExternalIp>(), $"{Version}/externalmtas" };
        }

        [Theory]
        [MemberData(nameof(RestApiModelEndpointSpecimens))]
        public async Task Should_request_to_the_expected_endpoint_name_for_model_type<T>(T model, string endpoint) where T : IRestApiModel
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = specimens.Create<HttpStatusCode>(),
            };

            var httpMessageHandlerMock = CreateHttpMessageHandlerMock(httpResponseMessage);
            var httpClientFactoryMock = CreateHttpClientFactoryMock(httpMessageHandlerMock.Object);
            var sut = CreateSut(httpClientFactory: httpClientFactoryMock.Object);

            // Act
            await sut.GetAsync<T>();
            await sut.PostAsync(model);
            await sut.DeleteAsync(model);

            // Assert
            httpMessageHandlerMock.Protected().Verify(
               nameof(HttpClient.SendAsync),
               Times.Exactly(3),
               ItExpr.Is<HttpRequestMessage>(request => request.RequestUri.AbsolutePath.EndsWith(endpoint)),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [Theory]
        [MemberData(nameof(RestApiModelSpecimens))]
        public async Task Get_should_return_http_status_code_on_sucessfull<T>(T model) where T : IRestApiModel
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = specimens.Create<HttpStatusCode>(),
            };

            var httpMessageHandlerMock = CreateHttpMessageHandlerMock(httpResponseMessage);
            var httpClientFactoryMock = CreateHttpClientFactoryMock(httpMessageHandlerMock.Object);
            var sut = CreateSut(httpClientFactory: httpClientFactoryMock.Object);

            // Act
            var result = await sut.GetAsync<T>();

            // Assert
            Assert.Equal(httpResponseMessage.StatusCode, result.HttpStatusCode);
        }

        [Theory]
        [MemberData(nameof(RestApiModelSpecimens))]
        public async Task Post_should_return_http_status_code_on_sucessfull(IRestApiModel model)
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = specimens.Create<HttpStatusCode>(),
            };

            var httpMessageHandlerMock = CreateHttpMessageHandlerMock(httpResponseMessage);
            var httpClientFactoryMock = CreateHttpClientFactoryMock(httpMessageHandlerMock.Object);
            var sut = CreateSut(httpClientFactory: httpClientFactoryMock.Object);

            // Act
            var result = await sut.PostAsync(model);

            // Assert
            Assert.Equal(httpResponseMessage.StatusCode, result.HttpStatusCode);
        }

        [Theory]
        [MemberData(nameof(RestApiModelSpecimens))]
        public async Task Delete_should_return_http_status_code_on_sucessfull(IRestApiModel model)
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = specimens.Create<HttpStatusCode>(),
            };

            var httpMessageHandlerMock = CreateHttpMessageHandlerMock(httpResponseMessage);
            var httpClientFactoryMock = CreateHttpClientFactoryMock(httpMessageHandlerMock.Object);
            var sut = CreateSut(httpClientFactory: httpClientFactoryMock.Object);

            // Act
            var result = await sut.DeleteAsync(model);

            // Assert
            Assert.Equal(httpResponseMessage.StatusCode, result.HttpStatusCode);
        }

        [Theory]
        [MemberData(nameof(RestApiModelSpecimens))]
        public async Task Get_should_throw_RestApiException_upon_an_exception_in_the_implementation<T>(T model) where T : IRestApiModel
        {
            // Arrange
            var exception = specimens.Create<Exception>();

            var httpMessageHandlerMock = CreateHttpMessageHandlerMock(exception);
            var httpClientFactoryMock = CreateHttpClientFactoryMock(httpMessageHandlerMock.Object);
            var sut = CreateSut(httpClientFactory: httpClientFactoryMock.Object);

            // Act
            var result = await Assert.ThrowsAsync<RestApiException>(() => sut.GetAsync<T>());

            // Assert
            Assert.Equal(exception, result.InnerException);
        }

        [Theory]
        [MemberData(nameof(RestApiModelSpecimens))]
        public async Task Post_should_throw_RestApiException_upon_an_exception_in_the_implementation(IRestApiModel model)
        {
            // Arrange
            var exception = specimens.Create<Exception>();

            var httpMessageHandlerMock = CreateHttpMessageHandlerMock(exception);
            var httpClientFactoryMock = CreateHttpClientFactoryMock(httpMessageHandlerMock.Object);
            var sut = CreateSut(httpClientFactory: httpClientFactoryMock.Object);

            // Act
            var result = await Assert.ThrowsAsync<RestApiException>(() => sut.PostAsync(model));

            // Assert
            Assert.Equal(exception, result.InnerException);
        }

        [Theory]
        [MemberData(nameof(RestApiModelSpecimens))]
        public async Task Delete_should_throw_RestApiException_upon_an_exception_in_the_implementation(IRestApiModel model)
        {
            // Arrange
            var exception = specimens.Create<Exception>();

            var httpMessageHandlerMock = CreateHttpMessageHandlerMock(exception);
            var httpClientFactoryMock = CreateHttpClientFactoryMock(httpMessageHandlerMock.Object);
            var sut = CreateSut(httpClientFactory: httpClientFactoryMock.Object);

            // Act
            var result = await Assert.ThrowsAsync<RestApiException>(() => sut.DeleteAsync(model));

            // Assert
            Assert.Equal(exception, result.InnerException);
        }

        [Theory]
        [MemberData(nameof(RestApiModelSpecimens))]
        public async Task Get_should_return_the_object_model_collection_on_sucessfull<T>(T model) where T : IRestApiModel
        {
            // Arrange
            var jsonListWithModel = JsonConvert.SerializeObject(new List<T> { model });
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonListWithModel)
            };
            var httpMessageHandlerMock = CreateHttpMessageHandlerMock(httpResponseMessage);
            var httpClientFactoryMock = CreateHttpClientFactoryMock(httpMessageHandlerMock.Object);
            var sut = CreateSut(httpClientFactory: httpClientFactoryMock.Object);

            // Act
            var result = await sut.GetAsync<T>();

            // Assert
            Assert.NotNull(result.Content);
        }
    }
}
