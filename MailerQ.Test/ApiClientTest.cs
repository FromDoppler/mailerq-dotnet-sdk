using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace MailerQ.ApiClient.Test
{
    public class ApiClientTest
    {
        private const string _url = "http://192.168.170.140";
        private const string _token = "sBftYte4RhcXe4BHL0PccgyGGdOrrYD13gOinbhNCeI6bLwv6nQz7BM1pfl8CdrnAPXZpMJnJxTvtDB4j4BW1xd3V1SksNFhA4xvPBOnS1IasoyeLqc3z6dcdC4PNqsGMRIPyTWZOqRYTJcFfvPnueGsVoq5bDcEujmY6lgD4tOKb4fe8c88JCM2vAr6P4cLmjJAA76IOkBKyj4ZwuFP2M2PTmbmEFayDoX2iQNKbeC4IiJQT6MDt3VAo8bM448";

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

        [InlineData(_url, _token, "v1", true)]
        [Theory(Skip = "true")]
        public void Post_errors_should_finish_sucessfull(string uri, string token, string version, bool expectedResult)
        {
            // Arrange
            var options = CreateSettings(uri, token, version);

            // Act
            var sut = new ApiClient(options, Mock.Of<ILogger<ApiClient>>());

            var error = new Error
            {
                Domain = "hotmail.com",
                Code = 421,
                Tag = "CampaignFromUTest10",
                Cluster = true
            };

            var result = sut.Post(error);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [InlineData(_url, "invalidtoken", "v1", true)]
        [Theory(Skip = "true")]
        public void Post_errors_should_throw_exception_unauthorized(string uri, string token, string version, bool expectedResult)
        {
            // Arrange
            var options = CreateSettings(uri, token, version);

            // Act
            var sut = new ApiClient(options, Mock.Of<ILogger<ApiClient>>());

            var error = new Error
            {
                Domain = "hotmail.com",
                Code = 421,
                Tag = "CampaignFromUTest3",
                Cluster = true
            };

            // Assert
            Assert.ThrowsAny<WebException>(() => sut.Post(error));
        }

        [InlineData(_url, _token, "v1", true)]
        [Theory(Skip = "true")]
        public void Post_pauses_should_finish_sucessfull(string uri, string token, string version, bool expectedResult)
        {
            // Arrange
            var options = CreateSettings(uri, token, version);

            // Act
            var sut = new ApiClient(options, Mock.Of<ILogger<ApiClient>>());

            var pause = new Pause
            {
                Domain = "hotmail.com",
                Tag = "PauseCampaignFromUTest"
            };

            var result = sut.Post(pause);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [InlineData(_url, _token, "v1", true)]
        [Theory(Skip = "true")]
        public void Post_inject_should_finish_sucessfull(string uri, string token, string version, bool expectedResult)
        {
            // Arrange
            var options = CreateSettings(uri, token, version);

            // Act
            var sut = new ApiClient(options, Mock.Of<ILogger<ApiClient>>());

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

            var result = sut.Post(inject);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [InlineData(_url, _token, "v1")]
        [Theory(Skip = "true")]
        public void Get_errors_should_finish_sucessfull(string uri, string token, string version)
        {
            // Arrange
            var options = CreateSettings(uri, token, version);

            // Act
            var sut = new ApiClient(options, Mock.Of<ILogger<ApiClient>>());

            var error = new Error();

            var result = sut.Get(error);

            // Assert
            Assert.NotNull(result);
        }

        [InlineData(_url, _token, "v1")]
        [Theory(Skip = "true")]
        public void Get_pauses_should_finish_sucessfull(string uri, string token, string version)
        {
            // Arrange
            var options = CreateSettings(uri, token, version);

            // Act
            var sut = new ApiClient(options, Mock.Of<ILogger<ApiClient>>());

            var pause = new Pause();

            var result = sut.Get(pause);

            // Assert
            Assert.NotNull(result);
        }

        [InlineData(_url, _token, "v1", true)]
        [Theory(Skip = "true")]
        public void Delete_errors_should_finish_sucessfull(string uri, string token, string version, bool expectedResult)
        {
            // Arrange
            var options = CreateSettings(uri, token, version);

            // Act
            var sut = new ApiClient(options, Mock.Of<ILogger<ApiClient>>());

            var error = new Error
            {
                Domain = "hotmail.com",
                Code = 421,
                Tag = "CampaignTestErrorApi",
                Cluster = true
            };

            var result = sut.Delete(error);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [InlineData(_url, _token, "v1", true)]
        [Theory(Skip = "true")]
        public void Delete_pauses_should_finish_sucessfull(string uri, string token, string version, bool expectedResult)
        {
            // Arrange
            var options = CreateSettings(uri, token, version);

            // Act
            var sut = new ApiClient(options, Mock.Of<ILogger<ApiClient>>());

            var pause = new Pause
            {
                Domain = "hotmail.com",
                Tag = "PauseTestApiClient2"
            };

            var result = sut.Delete(pause);

            // Assert
            Assert.Equal(result, expectedResult);
        }
    }
}
