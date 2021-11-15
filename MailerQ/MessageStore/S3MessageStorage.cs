using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailerQ.MessageStore
{
    internal class S3MessageStorage : IMessageStorage
    {
        private readonly IAmazonS3 _client;

        public S3MessageStorage(string url)
        {
            var config = new AmazonS3Config { ServiceURL = url };
            _client = new AmazonS3Client(config);
        }

        public async Task<string> InsertAsync(string message, int secondsToExpire, CancellationToken cancellationToken)
        {
            var key = Guid.NewGuid().ToString();
            var request = new PutObjectRequest
            {
                BucketName = IMessageStorage.DefaultStorageName,
                Key = key,
                ContentType = "text/plain",
                ContentBody = message,
            };

            await _client.PutObjectAsync(request, cancellationToken);

            return key;
        }
    }
}
