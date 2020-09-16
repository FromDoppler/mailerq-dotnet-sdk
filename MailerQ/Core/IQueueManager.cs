using System;

namespace MailerQ
{
    [Obsolete("Use IQueuePublisher and/or IQueueSubscriber")]
    public interface IQueueManager : IDisposable, IQueuePublisher, IQueueSubscriber
    {
    }
}
