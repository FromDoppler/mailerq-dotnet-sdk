using EasyNetQ;
using EasyNetQ.ConnectionString;
using MailerQ.Conventions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MailerQ
{
    /// <summary>
    /// A queue message publisher
    /// </summary>
    /// <remarks>Implemented with RabbitMQ.Client</remarks>
    public sealed class RabbitMQClientPublisher : IQueuePublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ConnectionConfiguration _settings;

        /// <summary>
        /// Initializes a new instance of queue message publisher
        /// </summary>
        /// <param name="options"></param>
        public RabbitMQClientPublisher(IOptions<MailerQConfiguration> options)
        {
            var connectionStringParser = new ConnectionStringParser();
            _settings = connectionStringParser.Parse(options.Value.RabbitConnectionString);

            var factory = new ConnectionFactory
            {
                VirtualHost = _settings.VirtualHost,
                UserName = _settings.UserName,
                Password = _settings.Password,
                ClientProvidedName = Assembly.GetEntryAssembly().GetName().Name,
            };

            if (!string.IsNullOrEmpty(options.Value.RabbitPassword))
            {
                factory.Password = options.Value.RabbitPassword;
            }

            var hosts = _settings.Hosts
                .Select(host => new AmqpTcpEndpoint { HostName = host.Host, Port = host.Port, Ssl = host.Ssl })
                .ToList();

            _connection = factory.CreateConnection(hosts);
            _channel = _connection.CreateModel();

            if (_settings.PublisherConfirms)
            {
                _channel.ConfirmSelect();
            }
        }

        /// <inheritdoc/>
        public void Publish(OutgoingMessage outgoingMessage, string queueName = QueueName.Outbox)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = _settings.PersistentMessages;
            properties.Priority = (byte)outgoingMessage.Priority;

            var body = CreateBody(outgoingMessage);

            _channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: false, properties, body);
            WaitConfirmation();
        }

        /// <inheritdoc/>
        public void Publish(IEnumerable<OutgoingMessage> outgoingMessages, string queueName = QueueName.Outbox)
        {
            var batch = _channel.CreateBasicPublishBatch();

            foreach (var outgoingMessage in outgoingMessages)
            {
                var properties = _channel.CreateBasicProperties();
                properties.Persistent = _settings.PersistentMessages;
                properties.Priority = (byte)outgoingMessage.Priority;

                var body = CreateBody(outgoingMessage);

                batch.Add(exchange: "", routingKey: queueName, mandatory: false, properties, body);
            }

            batch.Publish();
            WaitConfirmation();
        }

        private void WaitConfirmation()
        {
            if (_settings.PublisherConfirms)
            {
                var confirmed = _channel.WaitForConfirms(_settings.Timeout, out var timedout);
                if (timedout)
                {
                    throw new TimeoutException("No Ack or Nack recived before timeout");
                }
                if (!confirmed)
                {
                    throw new Exception("Message publish was explicitly Nacked");
                }
            }
        }

        /// <inheritdoc/>
        public Task PublishAsync(OutgoingMessage outgoingMessage, string queueName = QueueName.Outbox)
        {
            Publish(outgoingMessage, queueName);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task PublishAsync(IEnumerable<OutgoingMessage> outgoingMessages, string queueName = QueueName.Outbox)
        {
            Publish(outgoingMessages, queueName);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }

        private static ReadOnlyMemory<byte> CreateBody(OutgoingMessage outgoingMessage)
        {
            var json = JsonConvert.SerializeObject(outgoingMessage);
            var bytes = Encoding.UTF8.GetBytes(json);
            return bytes;
        }
    }
}
