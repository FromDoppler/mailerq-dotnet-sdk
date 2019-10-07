using EasyNetQ;
using EasyNetQ.Topology;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MailerQ
{
    public class QueueManager : IQueueManager
    {
        readonly IAdvancedBus bus;
        readonly MailerQConfiguration configuration;

        public QueueManager(MailerQConfiguration configuration)
        {
            this.configuration = configuration;
            bus = RabbitHutch.CreateBus(configuration.RabbitConnectionString).Advanced;
        }

        public QueueManager(IOptions<MailerQConfiguration> options) : this(options.Value) { }

        public void Publish(OutgoingMessage message)
        {
            var queueName = typeof(OutgoingMessage).GetAttribute<QueueAttribute>().QueueName;
            Publish(message, queueName);
        }

        public void Publish(OutgoingMessage outgoingMessage, string queueName)
        {
            var message = new Message<OutgoingMessage>(outgoingMessage);
            bus.Publish(Exchange.GetDefault(), queueName, false, message);
        }

        public async Task PublishAsync(OutgoingMessage message)
        {
            var queueName = typeof(OutgoingMessage).GetAttribute<QueueAttribute>().QueueName;
            await PublishAsync(message, queueName);
        }

        public async Task PublishAsync(OutgoingMessage outgoingMessage, string queueName)
        {
            var message = new Message<OutgoingMessage>(outgoingMessage);
            await bus.PublishAsync(Exchange.GetDefault(), queueName, false, message);
        }

        public IDisposable Subscribe<T>(Action<T> action) where T : OutgoingMessage
        {
            string queueName = GetQueueName<T>();
            return Subscribe(action, queueName);
        }

        public IDisposable Subscribe<T>(Action<T> action, string queueName) where T : OutgoingMessage
        {
            var queue = bus.QueueDeclare(queueName);
            return bus.Consume(queue, (body, properties, info) =>
            {
                string jsonMessage = Encoding.UTF8.GetString(body);
                var resultMessage = JsonConvert.DeserializeObject<T>(jsonMessage);
                action.Invoke(resultMessage);
            });
        }

        public IDisposable SubscribeAsync<T>(Func<T, Task> action) where T : OutgoingMessage
        {
            string queueName = GetQueueName<T>();
            return SubscribeAsync(action, queueName);
        }

        public IDisposable SubscribeAsync<T>(Func<T, Task> action, string queueName) where T : OutgoingMessage
        {
            var queue = bus.QueueDeclare(queueName);
            return bus.Consume(queue, async (body, properties, info) =>
            {
                string jsonMessage = Encoding.UTF8.GetString(body);
                var resultMessage = JsonConvert.DeserializeObject<T>(jsonMessage);
                await action.Invoke(resultMessage);
            });
        }

        string GetQueueName<T>()
        {
            string queueName = typeof(T).GetAttribute<QueueAttribute>().QueueName;
            return $"{configuration.QueuesNamePrefix}{queueName}";
        }

        public void Dispose()
        {
            bus.Dispose();
        }
    }
}
