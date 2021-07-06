using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace MailerQ
{
    /// <summary>
    /// DSN setting that says that a delivery status notification should be sent back to the original envelope address
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class DeliveryStatusNotification
    {
        /// <summary>
        /// Events type that trigger a notification
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum NotifyType
        {
            /// <summary>
            /// A failure delivery
            /// </summary>
            FAILURE,
            /// <summary>
            /// A delay delivery
            /// </summary>
            DELAY,
            /// <summary>
            /// A success delivery
            /// </summary>
            SUCCESS,
            /// <summary>
            /// A never delivery
            /// </summary>
            NEVER
        }

        /// <summary>
        /// Return type for Delivery Status Notification
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum RetType
        {
            /// <summary>
            /// Just the headers
            /// </summary>
            HDRS,
            /// <summary>
            /// Full original mail
            /// </summary>
            FULL,
        }

        /// <summary>
        /// Comma separated events that trigger a notification(FAILURE, DELAY, SUCCESS, NEVER)
        /// </summary>
        [JsonConverter(typeof(SingleOrArrayConverter<NotifyType>))]
        public ICollection<NotifyType> Notify { get; set; }

        /// <summary>
        /// Should the notification hold the full original mail or just the headers(FULL, HDRS)
        /// </summary>
        public RetType Ret { get; set; }

        /// <summary>
        /// The address to be included in the notification as "original-recipient"
        /// </summary>
        public string Orcpt { get; set; }

        /// <summary>
        /// Unique identifier to be included in the notification as "original-envelope-id"
        /// </summary>
        public string Envid { get; set; }
    }
}
