using Newtonsoft.Json;

namespace MailerQ
{
    [JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
    public abstract class Message
    {
        /// <summary>
        /// This is a custom property to allow follow the message while the delivery process
        /// </summary>
        public string DeliveryGuid { get; set; }
    }
}
