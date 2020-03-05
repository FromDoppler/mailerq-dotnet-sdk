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

        const string SimpleValidMongoUri = "mongodb://server/database";

        [InlineData(SimpleValidMongoUri)]
        [InlineData("mongodb://mongos1.example.com,mongos2.example.com/?readPreference=secondary")]
        [InlineData("mongodb://mongos1.example.com,mongos2.example.com/database?readPreference=secondary")]
        [Theory]
        public void Constructor_should_create_instance_with_valid_supported_engine_uri(string uri)
        {
            // Arrange
            var options = CreateOptions(uri);

            // Act
            var sut = new MessageStorage(options);

            // Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public async Task InsertAsync_should_throw_an_exception_using_mongodb_with_messages_larger_than_fifteen_megabytes()
        {
            // Arrange
            int size = 15728640; // 15 MB
            string message = new string('*', size);

            var options = CreateOptions(SimpleValidMongoUri);
            var sut = new MessageStorage(options);

            string expected = $"Message is to big, split is not suppported yet. Size should be less than {size} bytes.";

            // Act
            var exception = await Assert.ThrowsAsync<NotSupportedException>(() => sut.InsertAsync(message));

            // Assert
            Assert.Equal(expected, exception.Message);
        }
    }
}
