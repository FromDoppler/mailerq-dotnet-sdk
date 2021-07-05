﻿using MailerQ.Conventions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailerQ
{
    /// <summary>
    /// A queue message publisher
    /// </summary>
    public interface IQueuePublisher : IDisposable
    {
        /// <summary>
        /// Publish a outgoing message to the outbox queue
        /// </summary>
        /// <param name="outgoingMessage">The MailerQ outgoing message</param>
        /// <param name="queueName">The outbox custom queue name. Default: "outbox"</param>
        void Publish(OutgoingMessage outgoingMessage, string queueName = QueueName.Outbox);

        /// <summary>
        /// Publish a outgoing message to the outbox queue
        /// </summary>
        /// <param name="outgoingMessage">The MailerQ outgoing message</param>
        /// <param name="queueName">The outbox custom queue name. Default: "outbox"</param>
        Task PublishAsync(OutgoingMessage outgoingMessage, string queueName = QueueName.Outbox);

        /// <summary>
        /// Publish a list of outgoing messages to the outbox queue
        /// </summary>
        /// <param name="outgoingMessages">The MailerQ outgoing message</param>
        /// <param name="queueName">The outbox custom queue name. Default: "outbox"</param>
        void Publish(IEnumerable<OutgoingMessage> outgoingMessages, string queueName = QueueName.Outbox);

        /// <summary>
        /// Publish a list of outgoing messages to the outbox queue
        /// </summary>
        /// <param name="outgoingMessages">The MailerQ outgoing message</param>
        /// <param name="queueName">The outbox custom queue name. Default: "outbox"</param>
        Task PublishAsync(IEnumerable<OutgoingMessage> outgoingMessages, string queueName = QueueName.Outbox);
    }
}
