using Microsoft.Extensions.Logging;
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

namespace MailerQ.ApiClient.Test
{
    public class ApiClientTest
    {
        public static IOptions<ApiClientSettings> CreateSettings(string url, string token, string version)
        {
            var apiClientSettings = new ApiClientSettings
            {
                Url = url,
                Token = token,
                Version = version
            };
            return Options.Create(apiClientSettings);
        }

        [Fact]
        public async void Post_errors_should_finish_sucessfull()
        {
            // Arrange            
            var options = CreateSettings("url", "token", "version");
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    //Content = new StringContent("{'name':thecodebuzz,'city':'USA'}"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act
            var sut = new ApiClient(Mock.Of<ILogger<ApiClient>>(), options, httpClientFactoryMock.Object);

            var error = new Error
            {
                Domain = "hotmail.com",
                Code = 421,
                Tag = "CampaignFromUTest20",
                Cluster = true
            };

            var result = await sut.Post(error);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void Post_errors_should_fail_when_unauthorized()
        {
            // Arrange            
            var options = CreateSettings("url", "token", "version");
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act
            var sut = new ApiClient(Mock.Of<ILogger<ApiClient>>(), options, httpClientFactoryMock.Object);

            var error = new Error
            {
                Domain = "hotmail.com",
                Code = 421,
                Tag = "CampaignFromUTest3",
                Cluster = true
            };

            var result = await sut.Post(error);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void Post_pauses_should_finish_sucessfull()
        {
            // Arrange            
            var options = CreateSettings("url", "token", "version");
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act
            var sut = new ApiClient(Mock.Of<ILogger<ApiClient>>(), options, httpClientFactoryMock.Object);

            var pause = new Pause
            {
                Domain = "hotmail.com",
                Tag = "PauseCampaignFromUTest0304",
                Cluster = true
            };

            var result = await sut.Post(pause);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void Post_inject_should_finish_sucessfull()
        {
            // Arrange            
            var options = CreateSettings("url", "token", "version");
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act
            var sut = new ApiClient(Mock.Of<ILogger<ApiClient>>(), options, httpClientFactoryMock.Object);

            var inject = new Inject
            {
                Envelope = "bamaro@makingsense.com",
                Recipient = "bamaro@makingsense.com",
                Tags = new string[] { "TestInjectFromUTest2" },
                Mime = new Mime.Mime
                {
                    From = new Mime.EmailAddress { Address = "bamaro@makingsense.com" },
                    To = new List<Mime.EmailAddress>() { new Mime.EmailAddress { Address = "bamaro@makingsense.com" } },
                    Subject = "This is a MailerQ test from API Client",
                    Text = "This is a test from Api Client, to inject a new message",
                }
            };

            var result = await sut.Post(inject);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Get_errors_should_finish_sucessfull()
        {
            // Arrange            
            var listErrorsResult = JsonConvert.SerializeObject(new List<Error>(new[]
{
                new Error {
                    Code = 421,
                    Tag = "CampaignFromUTest01"
                },
                new Error {
                    Code = 421,
                    Tag = "CampaignFromUTest02"
                }
            }));

            var options = CreateSettings("url", "token", "version");
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(listErrorsResult),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act
            var sut = new ApiClient(Mock.Of<ILogger<ApiClient>>(), options, httpClientFactoryMock.Object);

            var result = sut.Get(new Error());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(JsonConvert.SerializeObject(result), listErrorsResult);
        }

        [Fact]
        public void Get_pauses_should_finish_sucessfull()
        {
            // Arrange            
            var listPausesResult = JsonConvert.SerializeObject(new List<Pause>(new[]
            {
                new Pause {
                    Tag = "CampaignFromUTest01"
                },
                new Pause {
                    Tag = "CampaignFromUTest02"
                }
            }));

            var options = CreateSettings("url", "token", "version");
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(listPausesResult),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act
            var sut = new ApiClient(Mock.Of<ILogger<ApiClient>>(), options, httpClientFactoryMock.Object);

            var result = sut.Get(new Pause());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(JsonConvert.SerializeObject(result), listPausesResult);
        }

        [Fact]
        public async void Delete_errors_should_finish_sucessfull()
        {
            // Arrange            
            var options = CreateSettings("url", "token", "version");
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act
            var sut = new ApiClient(Mock.Of<ILogger<ApiClient>>(), options, httpClientFactoryMock.Object);

            var error = new Error
            {
                Domain = "hotmail.com",
                Code = 421,
                Tag = "CampaignFromUTest",
                Cluster = true
            };

            var result = await sut.Delete(error);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void Delete_pauses_should_finish_sucessfull()
        {
            // Arrange            
            var options = CreateSettings("url", "token", "version");
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act
            var sut = new ApiClient(Mock.Of<ILogger<ApiClient>>(), options, httpClientFactoryMock.Object);

            var pause = new Pause
            {
                Domain = "hotmail.com",
                Tag = "PauseTestApiClient2",
                Cluster = true
            };

            var result = await sut.Delete(pause);

            // Assert
            Assert.True(result);
        }
    }
}
