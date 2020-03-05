using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MailerQ.MessageStorage.Test
{
    public class MessageStorageTest
    {
        public static IOptions<MessageStorageSettings> CreateOptions(string uri)
        {
            var messageStorageSettings = new MessageStorageSettings { Url = uri };
            return Options.Create(messageStorageSettings);
        }

        [InlineData("")]
        [InlineData("not uri")]
        [Theory]
        public void Constructor_should_throw_exception_with_invalid_uri(string uri)
        {
            // Arrange
            var options = CreateOptions(uri);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new MessageStorage(options));

            // Assert
            Assert.Contains("has invalid value", ex.Message);
        }


        [InlineData("http://server/path/to/directory")]
        [InlineData("https://server/path/to/directory")]
        [InlineData("ftp://path/to/directory")]
        [InlineData("file:///path/to/directory")]
        [InlineData("sqlite:///path/to/database/file")]
        [Theory]
        public void Constructor_should_throw_exception_with_unsupported_engine_uri(string uri)
        {
            // Arrange
            var options = CreateOptions(uri);
            var expected = $"{nameof(MessageStorageSettings)}.{nameof(MessageStorageSettings.Url)} has an unsupported message storage scheme";

            // Act
            var exception = Assert.Throws<ArgumentException>(() => new MessageStorage(options));

            // Assert
            Assert.Equal(expected, exception.Message);
        }

        [InlineData("couchbase://password@hostname/bucketname")]
        [InlineData("mysql://user:password@hostname/databasename")]
        [InlineData("postgresql://user:password@hostname/databasename")]
        [Theory]
        public void Create_should_throw_exception_with_not_implemented_supported_engine_uri(string uri)
        {
            // Arrange
            var options = CreateOptions(uri);
            var expected = "message storage engine is not implement yet.";

            // Act
            var ex = Assert.Throws<NotImplementedException>(() => new MessageStorage(options));

            // Assert
            Assert.Contains(expected, ex.Message);
        }
    }
}
