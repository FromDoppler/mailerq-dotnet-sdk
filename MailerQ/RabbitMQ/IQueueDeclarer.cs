using MailerQ.Conventions;
using System.Threading;
using System.Threading.Tasks;

namespace MailerQ.RabbitMQ
{
    /// <summary>
    /// Interfaz to declare queues
    /// </summary>
    public interface IQueueDeclarer
    {
        /// <summary>
        /// Declare a deadletter queue for publish OutgoingMessages
        /// </summary>
        /// <param name="name"></param>
        /// <param name="addQueueNamePrefix">Add prefix to queue name</param>
        /// <param name="durable"></param>
        /// <param name="messageTtl"></param>
        /// <param name="maxPriority"></param>
        /// <param name="deadLetterExchange"></param>
        /// <param name="deadLetterRoutingKey"></param>
        void DeclareDeadLetterQueueForPublish(
            string name,
            bool addQueueNamePrefix = true,
            bool durable = true,
            int messageTtl = 10,
            int maxPriority = (int)Priority.High,
            string deadLetterExchange = "",
            string deadLetterRoutingKey = QueueName.Outbox);

        /// <summary>
        /// Declare a deadletter queue for publish OutgoingMessages
        /// </summary>
        /// <param name="name"></param>
        /// <param name="addQueueNamePrefix">Add prefix to queue name</param>
        /// <param name="durable"></param>
        /// <param name="messageTtl"></param>
        /// <param name="maxPriority"></param>
        /// <param name="deadLetterExchange"></param>
        /// <param name="deadLetterRoutingKey"></param>
        /// <param name="cancellationToken">The cancellation token to propagate</param>
        Task DeclareDeadLetterQueueForPublishAsync(
            string name,
            bool addQueueNamePrefix = true,
            bool durable = true,
            int messageTtl = 10,
            int maxPriority = (int)Priority.High,
            string deadLetterExchange = "",
            string deadLetterRoutingKey = QueueName.Outbox,
            CancellationToken cancellationToken = default);
    }
}
