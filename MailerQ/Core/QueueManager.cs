using EasyNetQ;
using EasyNetQ.ConnectionString;
using EasyNetQ.Internals;
using EasyNetQ.Topology;
using MailerQ.Conventions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailerQ
{
    /// <summary>
    /// A queue manager
    /// </summary>
    /// <remarks>Implemented with EasyNetQ</remarks>
    public class QueueManager : IQueuePublisher, IQueueSubscriber, IQueueDeclarer
    {
        private readonly IAdvancedBus bus;
        private readonly MailerQConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of QueueManager
        /// </summary>
        /// <param name="configuration">The MailerQ configuration</param>
        public QueueManager(MailerQConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionConfiguration = new ConnectionStringParser().Parse(configuration.RabbitConnectionString);
            if (!string.IsNullOrWhiteSpace(configuration.RabbitPassword))
            {
                connectionConfiguration.Password = configuration.RabbitPassword;
            }

            bus = RabbitHutch.CreateBus(connectionConfiguration, x => { }).Advanced;
        }

        /// <summary>
        /// Initializes a new instance of QueueManager
        /// </summary>
        /// <param name="options">The MailerQ configuration as Option pattern</param>
        public QueueManager(IOptions<MailerQConfiguration> options) : this(options.Value) { }

        /// <inheritdoc/>
        public void DeclareDeadLetterQueueForPublish(
            string name,
            bool addQueueNamePrefix = true,
            bool durable = true,
            int messageTtl = 10,
            int maxPriority = (int)Priority.High,
            string deadLetterExchange = "",
            string deadLetterRoutingKey = QueueName.Outbox)
        {
            if (addQueueNamePrefix)
            {
                name = $"{configuration.QueuesNamePrefix}{name}";
            }

            bus.QueueDeclare(name: name, config =>
            {
                config
                    .AsDurable(true)
                    .WithMessageTtl(new TimeSpan(0, 0, 0, 0, messageTtl))
                    .WithMaxPriority(maxPriority)
                    .WithDeadLetterExchange(new Exchange(deadLetterExchange))
                    .WithDeadLetterRoutingKey(deadLetterRoutingKey);
            });
        }

        /// <inheritdoc/>
        public async Task DeclareDeadLetterQueueForPublishAsync(
            string name,
            bool addQueueNamePrefix = true,
            bool durable = true,
            int messageTtl = 10,
            int maxPriority = (int)Priority.High,
            string deadLetterExchange = "",
            string deadLetterRoutingKey = QueueName.Outbox,
            CancellationToken cancellationToken = default)
        {
            if (addQueueNamePrefix)
            {
                name = $"{configuration.QueuesNamePrefix}{name}";
            }

            await bus.QueueDeclareAsync(name: name, config =>
            {
                config
                    .AsDurable(durable)
                    .WithMessageTtl(new TimeSpan(0, 0, 0, 0, messageTtl))
                    .WithMaxPriority(maxPriority)
                    .WithDeadLetterExchange(new Exchange(deadLetterExchange))
                    .WithDeadLetterRoutingKey(deadLetterRoutingKey);
            }, cancellationToken);
        }

        /// <inheritdoc/>
        public void Publish(OutgoingMessage outgoingMessage, string queueName = QueueName.Outbox)
        {
            var message = CreateMessage(outgoingMessage);
            bus.Publish(Exchange.Default, queueName, false, message);
        }

        /// <inheritdoc/>
        public async Task PublishAsync(OutgoingMessage outgoingMessage, string queueName = QueueName.Outbox, CancellationToken cancellationToken = default)
        {
            var message = CreateMessage(outgoingMessage);
            await bus.PublishAsync(Exchange.Default, queueName, false, message, cancellationToken);
        }

        /// <inheritdoc/>
        public void Publish(IEnumerable<OutgoingMessage> outgoingMessages, string queueName = QueueName.Outbox)
        {
            foreach (var outgoingMessage in outgoingMessages)
            {
                Publish(outgoingMessage, queueName);
            }
        }

        /// <inheritdoc/>
        public async Task PublishAsync(IEnumerable<OutgoingMessage> outgoingMessages, string queueName = QueueName.Outbox, CancellationToken cancellationToken = default)
        {
            foreach (var outgoingMessage in outgoingMessages)
            {
                await PublishAsync(outgoingMessage, queueName, cancellationToken);
            }
        }

        private static Message<OutgoingMessage> CreateMessage(OutgoingMessage outgoingMessage)
        {
            var message = new Message<OutgoingMessage>(outgoingMessage);
            if (outgoingMessage.Priority.HasValue)
            {
                message.Properties.Priority = (byte)outgoingMessage.Priority;
            }
            return message;
        }

        /// <inheritdoc/>
        public IDisposable Subscribe<T>(Action<T> action) where T : OutgoingMessage
        {
            var queueName = GetQueueName<T>();
            return Subscribe(action, queueName);
        }

        /// <inheritdoc/>
        public IDisposable Subscribe<T>(Action<T> action, string queueName) where T : OutgoingMessage
        {
            var queue = bus.QueueDeclare(queueName);
            return bus.Consume(queue, (body, properties, info) =>
            {
                var jsonMessage = Encoding.UTF8.GetString(body.Span);
                var resultMessage = JsonConvert.DeserializeObject<T>(jsonMessage);
                action.Invoke(resultMessage);
            });
        }

        /// <inheritdoc/>
        public Task<IDisposable> SubscribeAsync<T>(Func<T, Task> action, CancellationToken cancellationToken = default) where T : OutgoingMessage
        {
            var queueName = GetQueueName<T>();
            return SubscribeAsync(action, queueName, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IDisposable> SubscribeAsync<T>(Func<T, Task> action, string queueName, CancellationToken cancellationToken = default) where T : OutgoingMessage
        {
            var queue = await bus.QueueDeclareAsync(queueName, cancellationToken);
            return bus.Consume(queue, async (body, properties, info) =>
            {
                var jsonMessage = Encoding.UTF8.GetString(body.Span);
                var resultMessage = JsonConvert.DeserializeObject<T>(jsonMessage);
                await action.Invoke(resultMessage);
            });
        }

        private string GetQueueName<T>()
        {
            var queueName = typeof(T).GetAttribute<QueueAttribute>().QueueName;
            return $"{configuration.QueuesNamePrefix}{queueName}";
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            bus.Dispose();
        }
    }
}
