﻿using MailerQ.Conventions;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailerQ.MessageStore
{
    public class MessageStorage : IMessageStorage
    {
        private readonly IMessageStorage storageEngine;

        public MessageStorage(IOptions<MailerQConfiguration> options)
        {
            var uri = options.Value.MessageStorageUrl;
            var schemeSeparatorIndex = uri.IndexOf(@"://");
            if (schemeSeparatorIndex <= 0)
            {
                throw new ArgumentException($"{nameof(MailerQConfiguration.MessageStorageUrl)} has invalid value");
            }

            var scheme = uri.Remove(schemeSeparatorIndex);
            if (!Enum.TryParse(scheme, ignoreCase: true, out StorageEngines storageScheme))
            {
                throw new ArgumentException($"{nameof(MailerQConfiguration.MessageStorageUrl)} value does not correspond to a supported storage engine");
            }
            storageEngine = CreateConcretStorage(storageScheme, uri);
        }

        private IMessageStorage CreateConcretStorage(StorageEngines storageEngine, string uri)
        {
            switch (storageEngine)
            {
                case StorageEngines.MongoDB:
                    return new MongoDBMessageStorage(uri);
                default:
                    throw new NotImplementedException($"{storageEngine} message storage engine is not implement yet.");
            }
        }

        public Task<string> InsertAsync(string message, int secondsToExpire = IMessageStorage.DefaultSecondsToExpire, CancellationToken cancellationToken = default)
        {
            return storageEngine.InsertAsync(message, secondsToExpire, cancellationToken);
        }
    }
}
