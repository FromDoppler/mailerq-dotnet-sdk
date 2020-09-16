using System;
using System.Threading.Tasks;

namespace MailerQ
{
    public interface IQueuePublisher : IDisposable
    {
        void Publish(OutgoingMessage message);
        void Publish(OutgoingMessage outgoingMessage, string queueName);
        Task PublishAsync(OutgoingMessage message);
        Task PublishAsync(OutgoingMessage outgoingMessage, string queueName);
    }
}
