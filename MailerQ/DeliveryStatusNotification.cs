using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace MailerQ
{
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class DeliveryStatusNotification
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum NotifyType
        {
            FAILURE,
            DELAY,
            SUCCESS,
            NEVER
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum RetType
        {
            HDRS,
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
