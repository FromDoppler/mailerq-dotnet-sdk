﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailerQ.MessageStore
{
    internal class MongoDBMessageStorage : IMessageStorage
    {
        private const int MessageMaxSuppportedSize = 15728640; // 15 MB
        private const int DaysToExpire = 7;
        private const string DataBase = "mailerq";
        private readonly string Collection = "message";
        private readonly IMongoCollection<BsonDocument> messages;

        public MongoDBMessageStorage(string url)
        {
            var mongoUrl = MongoUrl.Create(url);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName ?? DataBase);
            messages = database.GetCollection<BsonDocument>(Collection);
        }

        public async Task<string> InsertAsync(string message, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            var key = ObjectId.GenerateNewId(now).ToString();

            var encodedMessage = Encoding.UTF8.GetBytes(message);

            if (encodedMessage.Length >= MessageMaxSuppportedSize)
            {
                // TODO split into multiple documents to allow any message size
                throw new NotSupportedException($"Message is to big, split is not suppported yet. Size should be less than {MessageMaxSuppportedSize} bytes.");
            }

            var mime = new BsonDocument {
                { "_id", key },
                { "value", encodedMessage  },
                { "expire", now.AddDays(DaysToExpire) },
                { "modified", now },
                { "encoding", "identity" }
            };

            await messages.InsertOneAsync(mime, null, cancellationToken);
            return key;
        }
    }
}
