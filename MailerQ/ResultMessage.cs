using EasyNetQ;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MailerQ
{
    [Queue(Conventions.QueueName.Results)]
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore,
        ItemTypeNameHandling = TypeNameHandling.Auto
    )]
    public class ResultMessage : OutgoingMessage
    {
        public IList<DeliveryAttempt> Results { get; set; }
        public SentInfo Sent { get; set; }
    }

    [Queue(Conventions.QueueName.Success)]
    public class SuccessMessage : ResultMessage
    {
    }

    [Queue(Conventions.QueueName.Failure)]
    public class FailureMessage : ResultMessage
    {
    }

    [Queue(Conventions.QueueName.Retry)]
    public class RetryMessage : ResultMessage
    {
    }

    [Queue(Conventions.QueueName.Refused)]
    public class RefusedMessage : ResultMessage
    {
    }
}
