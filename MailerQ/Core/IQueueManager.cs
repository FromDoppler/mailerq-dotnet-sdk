using System;

namespace MailerQ
{
    /// <summary>
    /// A queue manager for publishing and subscribe messages to and from the queue
    /// </summary>
    [Obsolete("Use IQueuePublisher and/or IQueueSubscriber")]
    public interface IQueueManager : IDisposable, IQueuePublisher, IQueueSubscriber
    {
    }
}
