using System;
using System.Threading.Tasks;

namespace MailerQ
{
    public interface IQueueManager : IDisposable
    {
        void Publish(OutgoingMessage message);
        void Publish(OutgoingMessage outgoingMessage, string queueName);
        Task PublishAsync(OutgoingMessage message);
        Task PublishAsync(OutgoingMessage outgoingMessage, string queueName);
        IDisposable Subscribe<T>(Action<T> action) where T : OutgoingMessage;
        IDisposable Subscribe<T>(Action<T> action, string queueName) where T : OutgoingMessage;
        IDisposable SubscribeAsync<T>(Func<T, Task> action) where T : OutgoingMessage;
        IDisposable SubscribeAsync<T>(Func<T, Task> action, string queueName) where T : OutgoingMessage;
    }
}
