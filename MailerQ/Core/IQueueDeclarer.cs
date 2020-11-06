using MailerQ.Conventions;

namespace MailerQ
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
    }
}
