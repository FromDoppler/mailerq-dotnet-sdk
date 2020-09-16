using MailerQ.Conventions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailerQ
{
    public interface IQueuePublisher : IDisposable
    {
        void Publish(OutgoingMessage message);
        void Publish(OutgoingMessage outgoingMessage, string queueName);
        Task PublishAsync(OutgoingMessage message);
        Task PublishAsync(OutgoingMessage outgoingMessage, string queueName);
        void Publish(IEnumerable<OutgoingMessage> messages, string queueName = QueueName.Outbox);
        Task PublishAsync(IEnumerable<OutgoingMessage> messages, string queueName = QueueName.Outbox);
    }
}
