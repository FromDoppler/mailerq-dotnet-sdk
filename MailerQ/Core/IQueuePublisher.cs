using MailerQ.Conventions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailerQ
{
    public interface IQueuePublisher : IDisposable
    {
        void Publish(OutgoingMessage outgoingMessage, string queueName = QueueName.Outbox);
        Task PublishAsync(OutgoingMessage outgoingMessage, string queueName = QueueName.Outbox);
        void Publish(IEnumerable<OutgoingMessage> outgoingMessages, string queueName = QueueName.Outbox);
        Task PublishAsync(IEnumerable<OutgoingMessage> outgoingMessages, string queueName = QueueName.Outbox);
    }
}
