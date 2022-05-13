using EasyNetQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MailerQ
{
    /// <summary>
    /// A message that MailerQ must attempt to sent
    /// </summary>
    [Queue(Conventions.QueueName.Outbox)]
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class OutgoingMessage
    {
        /// <summary>
        /// Unique message id generated for the mail
        /// </summary>
        [JsonProperty("message-id")]
        public string MessageId { get; set; }

        /// <summary>
        /// Envelope("MAIL FROM") address
        /// </summary>
        public string Envelope { get; set; }

        /// <summary>
        /// Recipient("RCPT TO") address
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Full mime data to be sent
        /// </summary>
        public object Mime { get; set; }

        /// <summary>
        /// Key to the mime data in external storage
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Do not remove mime data after delivery
        /// </summary>
        public bool? KeepMime { get; set; }

        /// <summary>
        /// Personalization data
        /// </summary>
        public IDictionary<string, object> Data { get; set; }

        /// <summary>
        /// IP addresses to send the message from
        /// </summary>
        /// <remarks>
        /// Note that the pools are mutually exclusive with the IPs,
        /// that is, if the `ips` property is set in the JSON, the `pool` property will be ignored,
        /// as a specific set of IPs is more specific than a pool setting.
        /// </remarks>
        public ICollection<string> IPs { get; set; }

        /// <summary>
        /// Pool identifier to send the message from
        /// </summary>
        /// <remarks>
        /// Note that the pools are mutually exclusive with the IPs,
        /// that is, if the `ips` property is set in the JSON, the `pool` property will be ignored,
        /// as a specific set of IPs is more specific than a pool setting.
        /// </remarks>
        public string Pool { get; set; }

        /// <summary>
        /// Properties for when the next attempt should be
        /// </summary>
        public NextAttempt NextAttempt { get; set; }

        /// <summary>
        /// Time until which a delivery should be retried
        /// </summary>
        [JsonConverter(typeof(Json.DateTimeConverter))]
        public DateTime? MaxDeliverTime { get; set; }

        /// <summary>
        /// Max number of delivery attempts
        /// </summary>
        public int? MaxAttempts { get; set; }

        /// <summary>
        /// The delays between the delivery attempts
        /// </summary>
        public IList<int> Retries { get; set; }

        /// <summary>
        /// Force delivery, even when errors occur or conversion is impossible
        /// </summary>
        public bool? Force { get; set; }

        /// <summary>
        /// Turn style blocks in html mails into inline style attributes
        /// </summary>
        public bool? InLineCSS { get; set; }

        /// <summary>
        /// Private keys to sign the mail
        /// </summary>
        [JsonConverter(typeof(SingleOrArrayConverter<Dkim>))]
        public ICollection<Dkim> Dkim { get; set; }

        /// <summary>
        /// Settings for Delivery Status Notifications
        /// </summary>
        public DeliveryStatusNotification DSN { get; set; }

        /// <summary>
        /// Alternative rabbitmq queues for results
        /// </summary>
        public Queues Queues { get; set; }

        /// <summary>
        /// Smarthost settings
        /// </summary>
        public SmartHost SmartHost { get; set; }

        /// <summary>
        /// The tags to add to the message
        /// </summary>
        public ICollection<string> Tags { get; set; }

        /// <summary>
        /// Add or change the mime headers
        /// </summary>
        public MimeHeaders Headers { get; set; }

        /// <summary>
        /// The Priority of the message
        /// </summary>
        public int? Priority { get; set; }

        // TODO: verify if Extracted could be a more specify class insted object
        /// <summary>
        /// Values that MailerQ can extrac from the MIME message
        /// </summary>
        public object Extracted { get; set; }

        /// <summary>
        /// Date and time of the last time that MailerQ saw the message
        /// </summary>
        [JsonConverter(typeof(Json.DateTimeConverter))]
        public DateTime? Seen { get; set; }
    }
}
