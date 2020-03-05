using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailerQ.MessageStorage
{
    public class MessageStorage : IMessageStorage
    {
        private readonly IMessageStorage storage;

        public MessageStorage(IOptions<MessageStorageSettings> options)
        {
            var uri = options.Value.Url;
            int schemeSeparatorIndex = uri.IndexOf(@"://");
            if (schemeSeparatorIndex <= 0)
            {
                throw new ArgumentException($"{nameof(MessageStorageSettings)}.{nameof(MessageStorageSettings.Url)} has invalid value");
            }

            var scheme = uri.Remove(schemeSeparatorIndex);
            if (!Enum.TryParse(scheme, ignoreCase: true, out MessageStorageEngine storageScheme))
            {
                throw new ArgumentException($"{nameof(MessageStorageSettings)}.{nameof(MessageStorageSettings.Url)} has an unsupported message storage scheme");
            }
            storage = CreateConcretStorage(storageScheme, uri);
        }

        private IMessageStorage CreateConcretStorage(MessageStorageEngine storageEngine, string uri)
        {
            switch (storageEngine)
            {
                case MessageStorageEngine.MongoDB:
                    return new MongoMessageStorage(uri);
                default:
                    throw new NotImplementedException($"{storageEngine} message storage engine is not implement yet.");
            }
        }

        public Task<string> InsertAsync(string message, CancellationToken cancellationToken = default)
        {
            return storage.InsertAsync(message, cancellationToken);
        }
    }
}
