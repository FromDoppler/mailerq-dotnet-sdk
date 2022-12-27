using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailerQ
{
    /// <summary>
    /// Allow subscribe to a RabbitMQ queue and perform actions when a message arrive
    /// </summary>
    public interface IQueueSubscriber : IDisposable
    {
        /// <summary>
        /// Subscribe to a queue used by MailerQ for publishing messages using the name convention
        /// </summary>
        /// <typeparam name="T">Class derived from OutgoingMessage that have the </typeparam>
        /// <param name="action">Action to do with the message</param>
        /// <returns>Disposable for unsubscribe</returns>
        IDisposable Subscribe<T>(Action<T> action) where T : OutgoingMessage;

        /// <summary>
        /// Subscribe to a queue used by MailerQ for publishing messages
        /// </summary>
        /// <typeparam name="T">Class derived from OutgoingMessage that have the </typeparam>
        /// <param name="action">Action to do with the message</param>
        /// <param name="queueName">The name of the queue to subscribe</param>
        /// <returns>Disposable for unsubscribe</returns>
        IDisposable Subscribe<T>(Action<T> action, string queueName) where T : OutgoingMessage;

        /// <summary>
        /// Subscribe to a queue used by MailerQ for publishing messages using the name convention
        /// </summary>
        /// <typeparam name="T">Class derived from OutgoingMessage that have the </typeparam>
        /// <param name="action">Action to do with the message</param>
        /// <param name="cancellationToken">The cancellation token to propagate</param>
        /// <returns>Disposable for unsubscribe</returns>
        Task<IDisposable> SubscribeAsync<T>(Func<T, Task> action, CancellationToken cancellationToken = default) where T : OutgoingMessage;

        /// <summary>
        /// Subscribe to a queue used by MailerQ for publishing messages
        /// </summary>
        /// <typeparam name="T">Class derived from OutgoingMessage that have the </typeparam>
        /// <param name="action">Action to do with the message</param>
        /// <param name="queueName">The name of the queue to subscribe</param>
        /// <param name="cancellationToken">The cancellation token to propagate</param>
        /// <returns>Disposable for unsubscribe</returns>
        Task<IDisposable> SubscribeAsync<T>(Func<T, Task> action, string queueName, CancellationToken cancellationToken = default) where T : OutgoingMessage;
    }
}
