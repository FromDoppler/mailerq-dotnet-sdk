using EasyNetQ;
using EasyNetQ.ConnectionString;
using EasyNetQ.Topology;
using MailerQ.Conventions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailerQ
{
    public class QueueManager : IQueueManager
    {
        private readonly IAdvancedBus bus;
        private readonly MailerQConfiguration configuration;

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

        public QueueManager(IOptions<MailerQConfiguration> options) : this(options.Value) { }

        public void Publish(OutgoingMessage outgoingMessage, string queueName = QueueName.Outbox)
        {
            var message = CreateMessage(outgoingMessage);
            bus.Publish(Exchange.GetDefault(), queueName, false, message);
        }

        public async Task PublishAsync(OutgoingMessage outgoingMessage, string queueName = QueueName.Outbox)
        {
            var message = CreateMessage(outgoingMessage);
            await bus.PublishAsync(Exchange.GetDefault(), queueName, false, message);
        }

        public void Publish(IEnumerable<OutgoingMessage> messages, string queueName = QueueName.Outbox)
        {
            foreach (var message in messages)
            {
                Publish(message, queueName);
            }
        }

        public async Task PublishAsync(IEnumerable<OutgoingMessage> messages, string queueName = QueueName.Outbox)
        {
            foreach (var message in messages)
            {
                await PublishAsync(message, queueName);
            }
        }

        private static Message<OutgoingMessage> CreateMessage(OutgoingMessage outgoingMessage)
        {
            var message = new Message<OutgoingMessage>(outgoingMessage);
            if (outgoingMessage.Priority.HasValue)
            {
                message.Properties.Priority = (byte)outgoingMessage.Priority;
                message.Properties.PriorityPresent = outgoingMessage.Priority.HasValue;
            }
            return message;
        }

        public IDisposable Subscribe<T>(Action<T> action) where T : OutgoingMessage
        {
            var queueName = GetQueueName<T>();
            return Subscribe(action, queueName);
        }

        public IDisposable Subscribe<T>(Action<T> action, string queueName) where T : OutgoingMessage
        {
            var queue = bus.QueueDeclare(queueName);
            return bus.Consume(queue, (body, properties, info) =>
            {
                var jsonMessage = Encoding.UTF8.GetString(body);
                var resultMessage = JsonConvert.DeserializeObject<T>(jsonMessage);
                action.Invoke(resultMessage);
            });
        }

        public IDisposable SubscribeAsync<T>(Func<T, Task> action) where T : OutgoingMessage
        {
            var queueName = GetQueueName<T>();
            return SubscribeAsync(action, queueName);
        }

        public IDisposable SubscribeAsync<T>(Func<T, Task> action, string queueName) where T : OutgoingMessage
        {
            var queue = bus.QueueDeclare(queueName);
            return bus.Consume(queue, async (body, properties, info) =>
            {
                var jsonMessage = Encoding.UTF8.GetString(body);
                var resultMessage = JsonConvert.DeserializeObject<T>(jsonMessage);
                await action.Invoke(resultMessage);
            });
        }

        private string GetQueueName<T>()
        {
            var queueName = typeof(T).GetAttribute<QueueAttribute>().QueueName;
            return $"{configuration.QueuesNamePrefix}{queueName}";
        }

        public void Dispose()
        {
            bus.Dispose();
        }
    }
}
