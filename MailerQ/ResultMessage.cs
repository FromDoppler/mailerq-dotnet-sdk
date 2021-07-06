using EasyNetQ;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MailerQ
{
    /// <summary>
    /// A result generate by MailerQ when attempt delivery the message
    /// </summary>
    [Queue(Conventions.QueueName.Results)]
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore,
        ItemTypeNameHandling = TypeNameHandling.Auto
    )]
    public class ResultMessage : OutgoingMessage
    {
        /// <summary>
        /// The list of delivery attempts for the outgoing message
        /// </summary>
        public IList<DeliveryAttempt> Results { get; set; }
    }

    /// <summary>
    /// A result generate by MailerQ when then delivery attempt of the message is success
    /// </summary>
    [Queue(Conventions.QueueName.Success)]
    public class SuccessMessage : ResultMessage
    {
    }

    /// <summary>
    /// A result generate by MailerQ when then delivery attempt of the message is failure
    /// </summary>
    [Queue(Conventions.QueueName.Failure)]
    public class FailureMessage : ResultMessage
    {
    }

    /// <summary>
    /// A result generate by MailerQ when then delivery attempt of the message is a failure that can be retried
    /// </summary>
    [Queue(Conventions.QueueName.Retry)]
    public class RetryMessage : ResultMessage
    {
    }

    /// A result generate by MailerQ when then delivery attempt of the message is refused
    [Queue(Conventions.QueueName.Refused)]
    public class RefusedMessage : ResultMessage
    {
    }
}
